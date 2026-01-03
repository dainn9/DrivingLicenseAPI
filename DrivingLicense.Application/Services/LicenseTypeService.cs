using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.DTOs.LicenseType;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Application.Mappers;

namespace DrivingLicense.Application.Services
{
    public class LicenseTypeService : ILicenseTypeService
    {
        private readonly IUnitOfWork _uow;

        public LicenseTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<LicenseTypeDto> CreateAsync(LicenseTypeCreateDto dto)
        {
            var exists = await _uow.LicenseTypes.ExistsByNameAsync(excludeId: null, name: dto.Name);

            if (exists)
                throw new ConflictException("License type with the same name already exists.");

            var entity = dto.ToEntity();
            _uow.LicenseTypes.Add(entity);
            await _uow.CommitAsync();
            return entity.ToDto();
        }

        public async Task<List<LicenseTypeDto>> GetAllAsync()
        {
            var entities = await _uow.LicenseTypes.GetAllAsync();

            return entities.Select(e => e.ToDto()).ToList();
        }

        public async Task<LicenseTypeDto> GetByIdAsync(Guid id)
        {
            var entity = await _uow.LicenseTypes.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("License type not found.");

            return entity.ToDto();
        }

        public async Task UpdateAsync(Guid id, LicenseTypeUpdateDto dto)
        {
            var exists = await _uow.LicenseTypes.FindAsync(id);
            if (exists == null)
                throw new NotFoundException("License type not found.");

            var nameExists = await _uow.LicenseTypes.ExistsByNameAsync(excludeId: id, name: dto.Name);
            if (nameExists)
                throw new ConflictException("License type with the same name already exists.");

            exists.MapFromUpdateDto(dto);
            _uow.LicenseTypes.Update(exists);
            await _uow.CommitAsync();
        }
    }
}