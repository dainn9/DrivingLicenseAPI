using DrivingLicense.Domain.Entities.Common;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Entities
{
    public abstract class Person : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}