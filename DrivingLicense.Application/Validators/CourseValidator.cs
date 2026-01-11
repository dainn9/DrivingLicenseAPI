using DrivingLicense.Application.DTOs.Course;
using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.Validators
{
    public class CourseValidator
    {
        public static ValidationResult? ValidateDates(object _, ValidationContext context)
        {
            DateTime startDate;
            DateTime endDate;

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

            if (startDate.Date < DateTime.Today)
                return new ValidationResult("StartDate must be today or later.");

            if (startDate >= endDate)
                return new ValidationResult("EndDate must be after startdate.");

            return ValidationResult.Success;
        }
    }
}