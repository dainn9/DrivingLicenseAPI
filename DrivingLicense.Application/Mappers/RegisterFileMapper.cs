using DrivingLicense.Application.DTOs.RegisterFile;
using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Enums;
using DrivingLicense.Domain.Extensions;

namespace DrivingLicense.Application.Mappers
{
    public static class RegisterFileMapper
    {
        public static RegisterFileDto ToDto(this RegisterFile entity)
        {
            return new RegisterFileDto
            {
                Id = entity.Id,
                FullName = entity.Student?.FullName ?? string.Empty,
                SubmissionDate = entity.SubmissionDate,
                LicenseTypeName = entity.LicenseType?.LicenseTypeName ?? string.Empty,
                CourseName = entity.Course?.CourseName ?? string.Empty,
                IdentityCard = entity.Student?.IdentityCard ?? string.Empty,
                StatusName = entity.Status.ToText(),
            };
        }

        public static RegisterFileDetailDto ToDetailDto(this RegisterFile entity)
        {
            return new RegisterFileDetailDto
            {
                Id = entity.Id,
                FullName = entity.Student!.FullName,
                Gender = entity.Student.Gender.ToString(),
                DateOfBirth = entity.Student.DateOfBirth,
                PhoneNumber = entity.Student.PhoneNumber,
                Email = entity.Student.Email ?? string.Empty,
                Address = entity.Student.Address,
                Nationality = entity.Student.Nationality,
                IdentityCard = entity.Student.IdentityCard,
                LicenseTypeName = entity.LicenseType?.LicenseTypeName ?? string.Empty,
                CourseName = entity.Course?.CourseName ?? string.Empty,
                StatusName = entity.Status.ToText(),
                SubmissionDate = entity.SubmissionDate,
            };
        }

        public static RegisterFile ToEntity(this RegisterFileCreateDto dto)
        {
            return new RegisterFile
            {
                SubmissionDate = DateOnly.FromDateTime(DateTime.Now),
                ApplicationForm = true,
                Status = RegisterFileStatus.Created,
                StudentId = dto.StudentId,
                LicenseTypeId = dto.LicenseTypeId
            };
        }
    }
}
