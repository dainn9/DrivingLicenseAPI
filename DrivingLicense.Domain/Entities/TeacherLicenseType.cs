using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingLicense.Domain.Entities
{
    public class TeacherLicenseType
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid LicenseTypeId { get; set; }

        // Navigation properties
        public Teacher? Teacher { get; set; }
        public LicenseType? LicenseType { get; set; }
    }
}