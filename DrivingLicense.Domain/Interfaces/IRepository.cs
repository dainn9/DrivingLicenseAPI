using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrivingLicense.Domain.Entities.Common;

namespace DrivingLicense.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        void Add(T entity);
        void Update(T entity);
        // void Delete(T entity);
        // Task<bool> ExistsAsync(int id);
    }
}