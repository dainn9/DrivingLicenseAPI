using DrivingLicense.Domain.Entities.Common;

namespace DrivingLicense.Application.Interfaces
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