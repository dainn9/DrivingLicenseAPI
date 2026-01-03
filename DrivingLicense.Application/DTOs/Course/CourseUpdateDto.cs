using System.ComponentModel.DataAnnotations;
using DrivingLicense.Application.Validators;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.DTOs.Course
{
    public class CourseUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [CustomValidation(typeof(CourseValidator), nameof(CourseValidator.ValidateDates))]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int Capacity { get; set; }

        [Required]
        public CourseStatus Status { get; set; }

        [Required]
        public Guid LicenseTypeId { get; set; }
    }
}