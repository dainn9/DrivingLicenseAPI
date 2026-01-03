using DrivingLicense.Domain.Entities.Common;

namespace DrivingLicense.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> FindAsync(Guid id);
        void Add(T entity);
        void Update(T entity);
        // void Delete(T entity);
    }
}