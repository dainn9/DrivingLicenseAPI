using DrivingLicense.Application.DTOs.Auth.request;
using DrivingLicense.Application.DTOs.Auth.response;

namespace DrivingLicense.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(LoginResponseDto dto, string refreshToken)> LoginAsync(LoginRequestDto request);
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<(RefreshTokenResponseDto dto, string refreshToken)> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync();
    }
}
