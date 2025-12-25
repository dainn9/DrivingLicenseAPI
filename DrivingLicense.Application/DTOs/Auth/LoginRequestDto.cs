using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLicense.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}