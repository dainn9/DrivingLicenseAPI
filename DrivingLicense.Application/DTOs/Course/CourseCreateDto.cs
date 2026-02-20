using DrivingLicense.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.Course
{
    public class CourseCreateDto
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Course name cannot be empty or whitespace.")]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        [CustomValidation(typeof(CourseValidator), nameof(CourseValidator.ValidateDates))]
        public DateOnly EndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int Capacity { get; set; }

        [Required]
        public Guid LicenseTypeId { get; set; }
    }
}