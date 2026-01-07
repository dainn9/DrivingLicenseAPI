using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.Course;

namespace DrivingLicense.Application.Interfaces
{
    public interface ICourseService
    {
        Task<PagedResult<CourseDto>> GetPageAsync(PaginationParams pageParams);
        Task<CourseDto> GetByIdAsync(Guid id);
        Task<CourseDto> CreateAsync(CourseCreateDto dto);
        Task UpdateAsync(Guid id, CourseUpdateDto dto);
    }
}