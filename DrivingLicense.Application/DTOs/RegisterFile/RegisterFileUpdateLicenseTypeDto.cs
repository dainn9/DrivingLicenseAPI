using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.RegisterFile
{
    public class RegisterFileUpdateLicenseTypeDto
    {
        [Required]
        public Guid LicenseTypeId { get; set; }
    }
}
