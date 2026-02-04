using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.RegisterFile;

namespace DrivingLicense.Application.Interfaces
{
    public interface IRegisterFileService
    {
        Task<PagedResult<RegisterFileDto>> GetPageAsync(PaginationParams pageParams, string? searchTerm = null);
        Task<RegisterFileDto> GetByIdAsync(Guid id);
        Task<RegisterFileDetailDto> GetDetailByIdAsync(Guid id);
        Task<RegisterFileDto> CreateAsync(RegisterFileCreateDto dto);
        Task UnassignCourseAsync(Guid id);
        Task AssignCourseAsync(Guid id, RegisterFileUpdateCourseDto dto);
        Task UpdateLicenseTypeAsync(Guid id, RegisterFileUpdateLicenseTypeDto dto);
        Task UpdateStatusAsync(Guid id, RegisterFileUpdateStatusDto dto);
    }
}
