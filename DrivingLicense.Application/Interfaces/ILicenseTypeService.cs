using DrivingLicense.Application.DTOs.Common;
using DrivingLicense.Application.DTOs.LicenseType;

namespace DrivingLicense.Application.Interfaces
{
    public interface ILicenseTypeService
    {
        Task<List<LicenseTypeDto>> GetAllAsync();
        Task<LicenseTypeDto> GetByIdAsync(Guid id);
        Task<LicenseTypeDto> CreateAsync(LicenseTypeCreateDto dto);
        Task UpdateAsync(Guid id, LicenseTypeUpdateDto dto);
        Task<List<LookupDto>> GetDropDownListAsync();
        // Task DeleteAsync(Guid id);
    }
}