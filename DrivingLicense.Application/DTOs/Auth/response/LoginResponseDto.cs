using DrivingLicense.Application.DTOs.User;

namespace DrivingLicense.Application.DTOs.Auth.response
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public UserDto User { get; set; } = null!;
    }
}
