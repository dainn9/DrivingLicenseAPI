using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Entities
{
    public class TeachingSchedule
    {
        public Guid ScheduleId { get; set; }
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