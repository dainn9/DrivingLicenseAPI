
using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using DrivingLicense.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DrivingLicense.Infrastructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(DrivingDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null)
        {
            var normalized = name.Trim().ToUpper();
            return await _context.Courses.AsNoTracking().AnyAsync(c => c.CourseName.ToUpper() == normalized && (excludeId == null || c.Id != excludeId));
        }

        public async Task<(List<Course>, int)> GetPageAsync(int pageNumber, int pageSize)
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
    }
}