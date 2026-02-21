using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Domain.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<(List<Course>, int)> GetPageAsync(int pageNumber, int pageSize);
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<List<Course>> GetCourseByLicenseTypeIdAsync(Guid LicenseTypeId, CourseStatus courseStatus);
    }
}