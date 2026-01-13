namespace DrivingLicense.Application.DTOs.Student
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string IdentityCard { get; set; } = string.Empty;
    }
}
