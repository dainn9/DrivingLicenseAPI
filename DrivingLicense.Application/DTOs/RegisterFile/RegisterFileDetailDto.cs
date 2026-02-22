using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.DTOs.RegisterFile
{
    public class RegisterFileDetailDto
    {
        public Guid Id { get; set; }
        public StudentInfo StudentInfo { get; set; } = new StudentInfo();
        public Guid? LicenseTypeId { get; set; }
        public string? LicenseTypeName { get; set; }
        public Guid? CourseId { get; set; }
        public string? CourseName { get; set; }
        public string StatusCode { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateOnly SubmissionDate { get; set; }
    }
}
