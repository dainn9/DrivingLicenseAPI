namespace DrivingLicense.Application.DTOs.Auth.response
{
    public class RegisterResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}