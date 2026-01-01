using DrivingLicense.Application.DTOs.LicenseType;
using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Application.Mappers
{
    public static class LicenseTypeMapper
    {
        public static LicenseTypeDto ToDto(this LicenseType entity)
        {
            return new LicenseTypeDto
            {
                Id = entity.Id,
                Name = entity.LicenseTypeName,
                Description = entity.LicenseTypeDescription
            };
        }

        public static LicenseType ToEntity(this LicenseTypeCreateDto dto)
        {
            return new LicenseType
            {
                LicenseTypeName = dto.Name,
                LicenseTypeDescription = dto.Description
            };
        }

        public static void MapFromUpdateDto(this LicenseType entity, LicenseTypeUpdateDto dto)
        {
            entity.LicenseTypeName = dto.Name;
            entity.LicenseTypeDescription = dto.Description;
        }
    }
}