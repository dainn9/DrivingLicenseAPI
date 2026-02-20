using DrivingLicense.Application.DTOs.Course;
using DrivingLicense.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.Validators
{
    public class CourseValidator
    {
        public static ValidationResult? ValidateDates(object _, ValidationContext context)
        {
            DateOnly startDate;
            DateOnly endDate;

            switch (context.ObjectInstance)
            {
                case CourseCreateDto createDto:
                    startDate = createDto.StartDate;
                    endDate = createDto.EndDate;
                    break;
                case CourseUpdateDto updateDto:
                    startDate = updateDto.StartDate;
                    endDate = updateDto.EndDate;
                    break;
                default:
                    return ValidationResult.Success;
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (startDate < today)
                return new ValidationResult("StartDate must be today or later.");

            if (startDate >= endDate)
                return new ValidationResult("EndDate must be after startdate.");

            return ValidationResult.Success;
        }

        public static bool IsValidStatusTransition(CourseStatus current, CourseStatus next)
        {
            return current switch
            {
                CourseStatus.Open => next == CourseStatus.Ongoing || next == CourseStatus.Closed,
                CourseStatus.Ongoing => next == CourseStatus.Completed || next == CourseStatus.Closed,
                CourseStatus.Completed => next == CourseStatus.Closed,
                CourseStatus.Closed => false,
                _ => false,
            };
        }
    }
}