
using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DrivingLicense.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DrivingDbContext _context;
        public CourseRepository(DrivingDbContext context)
        {
            _context = context;
        }

        public void Add(Course entity)
            => _context.Courses.Add(entity);

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null)
        {
            var normalized = name.Trim().ToUpper();
            return await _context.Courses.AsNoTracking().AnyAsync(c => c.CourseName.ToUpper() == normalized && (excludeId == null || c.Id != excludeId));
        }

        public async Task<Course?> GetByIdAsync(Guid id)
            => await _context.Courses.AsNoTracking().Include(c => c.LicenseType).Include(c => c.RegisterFiles).AsSplitQuery().FirstOrDefaultAsync(c => c.Id == id);

        public void Update(Course entity)
            => _context.Courses.Update(entity);

        public async Task<(List<Course>, int)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Courses.AsNoTracking().Include(c => c.LicenseType).Include(c => c.RegisterFiles).OrderBy(c => c.Id);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsSplitQuery()
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<Course?> FindAsync(Guid id)
            => await _context.Courses.FindAsync(id);

    }
}