using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.DTOs.Student
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public StudentInfo StudentInfo { get; set; } = new StudentInfo();

    }
}
