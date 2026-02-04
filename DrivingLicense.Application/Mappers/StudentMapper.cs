using DrivingLicense.Application.DTOs.Student;
using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Application.Mappers
{
    public static class StudentMapper
    {
        public static StudentDto ToDto(this Student entity)
        {
            return new StudentDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Gender = entity.Gender,
                DateOfBirth = entity.DateOfBirth,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Address = entity.Address,
                Nationality = entity.Nationality,
                IdentityCard = entity.IdentityCard,
            };
        }

        public static Student ToEntity(this StudentCreateDto dto)
        {
            return new Student
            {
                FullName = dto.FullName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                Nationality = dto.Nationality,
                IdentityCard = dto.IdentityCard,
            };
        }

        public static void MapFromUpdateDto(this Student entity, StudentUpdateDto dto)
        {
            entity.FullName = dto.FullName;
            entity.Gender = dto.Gender;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.Address = dto.Address;
            entity.Nationality = dto.Nationality;
            entity.IdentityCard = dto.IdentityCard;
        }
    }
}