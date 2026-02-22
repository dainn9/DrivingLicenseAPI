using DrivingLicense.Application.DTOs.Course;
using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<(List<Course>, int)> GetPageAsync(int pageNumber, int pageSize);
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<List<CourseDropDownDto>> GetCourseByLicenseTypeIdAsync(Guid licenseTypeId, CourseStatus courseStatus);
        Task<CourseDto?> GetDetailAsync(Guid id);
    }
}