using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities
{
    public class ExamRegistration
    {
        public Guid ExamRegisId { get; set; }
        public Guid FileId { get; set; }
        public Guid ExamSessionId { get; set; }

        // Navigation properties
        public RegisterFile? RegisterFile { get; set; }
        public ExamSession? ExamSession { get; set; }
        public ExamResult? ExamResult { get; set; }

    }
}