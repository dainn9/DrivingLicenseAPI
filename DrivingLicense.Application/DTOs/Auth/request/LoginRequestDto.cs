using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.Auth.request

{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}