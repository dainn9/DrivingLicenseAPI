using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.DTOs.Auth.request;
using DrivingLicense.Application.DTOs.Auth.response;
using DrivingLicense.Application.DTOs.User;
using DrivingLicense.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DrivingLicense.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly JwtConfig _jwtConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IOptions<JwtConfig> jwtOptions, IHttpContextAccessor httpContextAccessor, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _jwtConfig = jwtOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _refreshTokenService = refreshTokenService;
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

            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user.Id);

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

            return (dto, refreshToken);
        }

        public async Task LogoutAsync()
        {
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return;

            await _refreshTokenService.ValidateAndRevokeAsync(refreshToken);
        }

        public async Task<(RefreshTokenResponseDto dto, string refreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var (userId, newRefreshToken) = await _refreshTokenService.RotateRefreshTokenAsync(refreshToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException("User not exists");

            var userRoles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _tokenService.GenerateToken(user.Id, user.UserName!, userRoles);

            var expiresIn = _jwtConfig.AccessTokenExpirationMinutes * 60;

            var dto = new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
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
