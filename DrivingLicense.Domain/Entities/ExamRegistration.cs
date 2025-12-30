using DrivingLicense.Domain.Entities.Common;

namespace DrivingLicense.Domain.Entities
{
    public class ExamRegistration : BaseEntity
    {
        public Guid FileId { get; set; }
        public Guid ExamSessionId { get; set; }

        // Navigation properties
        public RegisterFile? RegisterFile { get; set; }
        public ExamSession? ExamSession { get; set; }
        public ExamResult? ExamResult { get; set; }

    }
}