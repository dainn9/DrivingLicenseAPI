using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.DTOs.RegisterFile
{
    public class RegisterFileDetailDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string IdentityCard { get; set; } = string.Empty;
        public string? LicenseTypeName { get; set; }
        public string? CourseName { get; set; }
        public string StatusCode { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateOnly SubmissionDate { get; set; }
    }
}
