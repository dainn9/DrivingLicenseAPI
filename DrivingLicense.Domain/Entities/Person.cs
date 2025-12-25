using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}