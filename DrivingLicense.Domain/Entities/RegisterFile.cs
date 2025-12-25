using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Entities
{
    public class RegisterFile
    {
        public Guid RegisterFileId { get; set; }
        public DateOnly SubmissionDate { get; set; }
        public string? PortraitPhoto { get; set; }
        public string? IdentityDoc { get; set; }
        public string? HealthCert { get; set; }
        // Đã tạo hồ sơ, đang chờ duyệt, đã duyệt
        public RegisterFileStatus Status { get; set; }
        public bool ApplicationForm { get; set; }
        public Guid? LicenseTypeId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? CourseId { get; set; }

        // Navigation properties
        public LicenseType? LicenseType { get; set; }
        public Student? Student { get; set; }
        public Course? Course { get; set; }
        public ICollection<ExamRegistration> ExamRegistrations { get; set; } = new List<ExamRegistration>();
    }
}