using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.DTOs.Auth.request;
using DrivingLicense.Application.DTOs.Auth.response;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {

            var result = await _authService.LoginAsync(request);
            return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result));
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(ApiResponse<RegisterResponseDto>.SuccessResponse(result));
        }
    }
}
