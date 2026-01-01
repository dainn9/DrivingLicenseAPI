using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Application.DTOs.LicenseType
{
    public class LicenseTypeCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
    }
}