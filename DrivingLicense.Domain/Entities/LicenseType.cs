using DrivingLicense.Domain.Entities.Common;

namespace DrivingLicense.Domain.Entities
{
    public class LicenseType : BaseEntity
    {
        public string LicenseTypeName { get; set; } = null!;
        public string LicenseTypeDescription { get; set; } = null!;

        // Navigation properties
        public ICollection<RegisterFile> RegisterFiles { get; set; } = new List<RegisterFile>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<ExamSession> ExamSessions { get; set; } = new List<ExamSession>();
        public ICollection<TeacherLicenseType> TeacherLicenseTypes { get; set; } = new List<TeacherLicenseType>();

    }
}