using DrivingLicense.Application.DTOs.Course;
using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Extensions;

namespace DrivingLicense.Application.Mappers
{
    public static class CourseMapper
    {
        public static CourseDto ToDto(this Course entity)
        {
            return new CourseDto
            {
                Id = entity.Id,
                CourseName = entity.CourseName,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Capacity = entity.Capacity,
                CurrentStudentCount = entity.RegisterFiles.Count,
                Status = entity.Status.ToText(),
                LicenseTypeId = entity.LicenseTypeId,
                LicenseTypeName = entity.LicenseType?.LicenseTypeName ?? string.Empty,
            };
        }

        public static Course ToEntity(this CourseCreateDto dto)
        {
            return new Course
            {
                CourseName = dto.CourseName,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Capacity = dto.Capacity,
                Status = dto.Status,
                LicenseTypeId = dto.LicenseTypeId
            };
        }

        public static void MapFromUpdateDto(this Course entity, CourseUpdateDto dto)
        {
            entity.CourseName = dto.CourseName;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.Capacity = dto.Capacity;
            entity.Status = dto.Status;
            entity.LicenseTypeId = dto.LicenseTypeId;
        }
    }
}