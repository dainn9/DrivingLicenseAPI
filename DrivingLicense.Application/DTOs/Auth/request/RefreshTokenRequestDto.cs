using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.Auth.request
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}