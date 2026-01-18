using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.RegisterFile
{
    public class RegisterFileCreateDto
    {
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public Guid LicenseTypeId { get; set; }
    }
}
