using DrivingLicense.Domain.Enums;

public class StudentInfo
{
    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string IdentityCard { get; set; } = string.Empty;
}