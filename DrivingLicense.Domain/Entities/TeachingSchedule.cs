using DrivingLicense.Domain.Entities.Common;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Entities
{
    public class TeachingSchedule : BaseEntity
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateOnly TeachingDate { get; set; }
        public TeachingTypeStatus TeachingType { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Location { get; set; } = null!;

        // Navigation
        public Teacher? Teacher { get; set; }
        public Course? Course { get; set; }
    }
}