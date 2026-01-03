using DrivingLicense.Application.DTOs.LicenseType;

namespace DrivingLicense.Application.Interfaces
{
    public interface ILicenseTypeService
    {
        Task<List<LicenseTypeDto>> GetAllAsync();
        Task<LicenseTypeDto> GetByIdAsync(Guid id);
        Task<LicenseTypeDto> CreateAsync(LicenseTypeCreateDto dto);
        Task UpdateAsync(Guid Id, LicenseTypeUpdateDto dto);
        // Task DeleteAsync(Guid id);
    }
}