using System.ComponentModel.DataAnnotations;

namespace DrivingLicense.Application.DTOs.RegisterFile
{
    public class RegisterFileUpdateCourseDto
    {
        [Required]
        public Guid CourseId { get; set; }
    }
}
