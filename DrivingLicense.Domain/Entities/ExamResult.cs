using DrivingLicense.Domain.Entities.Common;

namespace DrivingLicense.Domain.Entities
{
    public class ExamResult : BaseEntity
    {
        public Guid ExamRegisId { get; set; }
        public int TheoryScore { get; set; }
        public int PracticalScore { get; set; }
        public bool IsCriticalMistake { get; set; } // true nếu phạm lỗi điểm liệt
        public bool Status { get; set; } // "true Đậu" hoặc "false Rớt"

        // Navigation properties
        public ExamRegistration? ExamRegistration { get; set; }
    }
}