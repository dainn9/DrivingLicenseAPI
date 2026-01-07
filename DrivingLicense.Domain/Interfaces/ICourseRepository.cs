using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Domain.Interfaces
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<(List<Course>, int)> GetPageAsync(int pageNumber, int pageSize);
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
    }
}