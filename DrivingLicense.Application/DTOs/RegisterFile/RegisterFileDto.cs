namespace DrivingLicense.Application.DTOs.RegisterFile
{
    public class RegisterFileDto
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public DateOnly SubmissionDate { get; set; }
        public string? LicenseTypeName { get; set; }
        public string? CourseName { get; set; }
        public string IdentityCard { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
    }
}