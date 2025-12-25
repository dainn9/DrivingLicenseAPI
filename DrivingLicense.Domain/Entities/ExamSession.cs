using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities
{
    public class ExamSession
    {
        public Guid ExamSessionId { get; set; }
        public string ExamSessionName { get; set; } = null!;
        public DateOnly ExamDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool Status { get; set; }
        public string Location { get; set; } = null!;
        public Guid LicenseTypeId { get; set; }

        // Navigation properties
        public LicenseType? LicenseType { get; set; }
        public ICollection<ExamRegistration> ExamRegistrations { get; set; } = new List<ExamRegistration>();

    }
}