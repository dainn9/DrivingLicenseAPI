using DrivingLicense.Application.DTOs.Auth;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid username or password.");
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.GenerateToken(user.Id, user.UserName!, userRoles);

                var response = new LoginResponseDto
                {
                    AccessToken = token,
                };

                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
                ModelState.AddModelError("Username", "User name is already taken.");

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
                ModelState.AddModelError("Email", "Email is already registered.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = new AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var role = request.IsAdmin ? "Administrator" : "Staff";

            var roleResult = await _userManager.AddToRoleAsync(newUser, role);

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            var token = _tokenService.GenerateToken(newUser.Id, newUser.UserName!, new List<string> { role });

            var response = new RegisterResponseDto
            {
                AccessToken = token,
                Username = newUser.UserName,
                Email = newUser.Email
            };

            return Ok(response);
        }
    }
}
