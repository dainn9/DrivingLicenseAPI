using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Application.DTOs.Auth
{
    public class RegisterResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}