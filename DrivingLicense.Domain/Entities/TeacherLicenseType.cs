namespace DrivingLicense.Domain.Entities
{
    public class TeacherLicenseType
    {
        public Guid TeacherId { get; set; }
        public Guid LicenseTypeId { get; set; }

        // Navigation properties
        public Teacher? Teacher { get; set; }
        public LicenseType? LicenseType { get; set; }
    }
}