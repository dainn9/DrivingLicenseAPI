using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.LicenseType
{
    public class LicenseTypeUpdateDto
    {
        [MaxLength(50)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Field cannot be empty or whitespace.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Field cannot be empty or whitespace.")]
        public string Description { get; set; } = string.Empty;
    }
}