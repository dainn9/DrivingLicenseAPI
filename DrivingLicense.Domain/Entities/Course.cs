using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Entities
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public CourseStatus Status { get; set; }
        public Guid LicenseTypeId { get; set; }

        // Navigation properties
        public LicenseType? LicenseType { get; set; }
        public ICollection<RegisterFile> RegisterFiles { get; set; } = new List<RegisterFile>();
        public ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
    }
}
