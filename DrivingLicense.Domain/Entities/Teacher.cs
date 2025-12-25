using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities
{
    public class Teacher : Person
    {
        // Navigation properties
        public ICollection<TeacherLicenseType> TeacherLicenseTypes { get; set; } = new List<TeacherLicenseType>();
        public ICollection<TeachingSchedule> TeachingSchedules { get; set; } = new List<TeachingSchedule>();
    }
}