namespace DrivingLicense.Application.DTOs.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int CurrentStudentCount { get; set; }
        public string Status { get; set; } = string.Empty;

        public Guid LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; } = string.Empty;
    }
}