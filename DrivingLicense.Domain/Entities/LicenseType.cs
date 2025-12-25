using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities
{
    public class LicenseType
    {
        public Guid LicenseTypeId { get; set; }
        public string LicenseTypeName { get; set; } = null!;
        public string LicenseTypeDescription { get; set; } = null!;

        // Navigation properties
        public ICollection<RegisterFile> RegisterFiles { get; set; } = new List<RegisterFile>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<ExamSession> ExamSessions { get; set; } = new List<ExamSession>();
        public ICollection<TeacherLicenseType> TeacherLicenseTypes { get; set; } = new List<TeacherLicenseType>();

    }
}