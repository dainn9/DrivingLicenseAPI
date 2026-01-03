using DrivingLicense.Application.DTOs.Course;
using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.Validators
{
    public class CourseValidator
    {
        public static ValidationResult? ValidateDates(object _, ValidationContext context)
        {
            var instance = context.ObjectInstance;

            switch (instance)
            {
                case CourseCreateDto createDto:
                    if (createDto.EndDate <= createDto.StartDate)
                        return new ValidationResult("EndDate must be after StartDate.");
                    if (createDto.EndDate <= DateTime.Today)
                        return new ValidationResult("EndDate must be after today.");
                    break;
                case CourseUpdateDto updateDto:
                    if (updateDto.EndDate <= updateDto.StartDate)
                        return new ValidationResult("EndDate must be after StartDate.");
                    if (updateDto.EndDate <= DateTime.Today)
                        return new ValidationResult("EndDate must be after today.");
                    break;
            }

            return ValidationResult.Success;
        }
    }
}