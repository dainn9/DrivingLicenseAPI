using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.Student;

namespace DrivingLicense.Application.Interfaces
{
    public interface IStudentService
    {
        Task<PagedResult<StudentDto>> GetPageAsync(PaginationParams pageParams, string? searchTerm = null);
        Task<StudentDto> GetByIdAsync(Guid id);
        Task<StudentDto> CreateAsync(StudentCreateDto dto);
        Task UpdateAsync(Guid id, StudentUpdateDto dto);
    }
}