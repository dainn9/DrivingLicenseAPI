using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.DTOs.Auth.request;
using DrivingLicense.Application.DTOs.Auth.response;
using DrivingLicense.Application.DTOs.User;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DrivingLicense.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly DrivingDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly JwtConfig _jwtConfig;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, DrivingDbContext context, IOptions<JwtConfig> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _context = context;
            _jwtConfig = jwtOptions.Value;
        }

        public async Task<(LoginResponseDto dto, string refreshToken)> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                throw new UnauthorizedException("Invalid username or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedException("Invalid username or password.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user.Id, user.UserName!, userRoles);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var hashedRefreshToken = _tokenService.HashToken(refreshToken);

            var refreshTokenEnity = new RefreshToken
            {
                Token = hashedRefreshToken,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            var expiresIn = _jwtConfig.AccessTokenExpirationMinutes * 60;

            var dto = new LoginResponseDto
            {
                AccessToken = token,
                ExpiresIn = expiresIn,
                User = new UserDto
                {
                    Id = user.Id,
                    Role = userRoles.ToList()

                }
            };

            _context.RefreshTokens.Add(refreshTokenEnity);
            await _context.SaveChangesAsync();
            return (dto, refreshToken);
        }

        public async Task<(RefreshTokenResponseDto dto, string refreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var hasedToken = _tokenService.HashToken(refreshToken);
            var storedRefreshToken = await _context.RefreshTokens.Include(rf => rf.User).FirstOrDefaultAsync(rf => rf.Token == hasedToken);

            if (storedRefreshToken == null
                 || storedRefreshToken.IsRevoked
                 || storedRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }

            var user = storedRefreshToken.User;
            var userRoles = await _userManager.GetRolesAsync(user);
            var newToken = _tokenService.GenerateToken(user.Id, user.UserName!, userRoles);

            storedRefreshToken.IsRevoked = true;
            storedRefreshToken.RevokedAt = DateTime.UtcNow;

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var hashedNewRefreshToken = _tokenService.HashToken(newRefreshToken);

            var newRefreshTokenEnity = new RefreshToken
            {
                Token = hashedNewRefreshToken,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(newRefreshTokenEnity);
            await _context.SaveChangesAsync();

            var expiresIn = _jwtConfig.AccessTokenExpirationMinutes * 60;

            var dto = new RefreshTokenResponseDto
            {
                AccessToken = newToken,
                ExpiresIn = expiresIn,
            };

            return (dto, newRefreshToken);
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null || existingEmail != null)
                throw new BadRequestException("User name or Email is already registered.");

            var newUser = new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException(result.Errors.Select(e => e.Description).ToList());

            var role = request.IsAdmin ? "Administrator" : "Staff";

            var roleResult = await _userManager.AddToRoleAsync(newUser, role);

            if (!roleResult.Succeeded)
                throw new BadRequestException(roleResult.Errors.Select(e => e.Description).ToList());

            var token = _tokenService.GenerateToken(newUser.Id, newUser.UserName!, new List<string> { role });

            var dto = new RegisterResponseDto
            {
                AccessToken = token,
                Username = newUser.UserName,
                Email = newUser.Email
            };

            return dto;
        }
    }
}
