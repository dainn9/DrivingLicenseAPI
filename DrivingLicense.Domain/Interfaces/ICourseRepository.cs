using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<(List<Course>, int)> GetAllAsync(int pageNumber, int pageSize);
        Task<Course?> GetByIdAsync(Guid id);
        void Add(Course entity);
        void Update(Course entity);
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
        Task<Course?> FindAsync(Guid id);
    }
}