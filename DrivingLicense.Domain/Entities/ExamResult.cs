using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities
{
    public class ExamResult
    {
        public Guid ResultId { get; set; }
        public Guid ExamRegisId { get; set; }
        public int TheoryScore { get; set; }
        public int PracticalScore { get; set; }
        public bool IsCriticalMistake { get; set; } // true nếu phạm lỗi điểm liệt
        public bool Status { get; set; } // "true Đậu" hoặc "false Rớt"

        // Navigation properties
        public ExamRegistration? ExamRegistration { get; set; }
    }
}