using DrivingLicense.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.Student
{
    public class StudentCreateDto
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Field cannot be empty or whitespace.")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public Gender Gender { get; set; } = Gender.Male;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(50)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(200)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Field cannot be empty or whitespace.")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@".*\S.*", ErrorMessage = "Field cannot be empty or whitespace.")]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        [MaxLength(12)]
        public string IdentityCard { get; set; } = string.Empty;
    }
}