using DrivingLicense.Domain.Entities.Common;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DrivingLicense.Infrastructure.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DrivingDbContext _context;
        public GenericRepository(DrivingDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
            => _context.Set<T>().Add(entity);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public Task<T?> GetByIdAsync(Guid id)
            => _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);
    }
}