using DrivingLicense.Application.Validators;
using DrivingLicense.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.Course
{
    public class CourseCreateDto
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Field cannot be empty or whitespace.")]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [CustomValidation(typeof(CourseValidator), nameof(CourseValidator.ValidateDates))]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Capacity { get; set; }

        [Required]
        public CourseStatus Status { get; set; }

        [Required]
        public Guid LicenseTypeId { get; set; }
    }
}