using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.DTOs.Auth;
using DrivingLicense.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DrivingLicense.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                throw new UnauthorizedException("Invalid username or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedException("Invalid username or password.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user.Id, user.UserName!, userRoles);

            var dto = new LoginResponseDto
            {
                AccessToken = token,
            };
            return dto;
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
