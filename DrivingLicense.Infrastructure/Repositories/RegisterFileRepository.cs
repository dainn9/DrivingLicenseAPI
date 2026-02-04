using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using DrivingLicense.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DrivingLicense.Infrastructure.Repositories
{
    public class RegisterFileRepository : GenericRepository<RegisterFile>, IRegisterFileRepository
    {
        public RegisterFileRepository(DrivingDbContext context) : base(context)
        {
        }

        public async Task<int> CountByCourseIdAsync(Guid courseId, Guid? excludeId = null)
            => await _context.RegisterFiles.AsNoTracking().CountAsync(rf => rf.CourseId == courseId && (excludeId == null || rf.Id != excludeId));

        public async Task<RegisterFile?> GetDetailByIdAsync(Guid id)
            => await _context.RegisterFiles.AsNoTracking().Include(rf => rf.Student).Include(rf => rf.LicenseType).Include(rf => rf.Course).AsSplitQuery().FirstOrDefaultAsync(rf => rf.Id == id);

        public async Task<(List<RegisterFile>, int)> GetPageAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var query = _context.RegisterFiles.AsNoTracking().AsSplitQuery().Include(rf => rf.LicenseType).Include(rf => rf.Student).Include(rf => rf.Course).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(rf => rf.Student!.IdentityCard.StartsWith(searchTerm) ||
                                          EF.Functions.Like(rf.Student.FullName, $"{searchTerm}%"));

            query = query.OrderBy(r => r.SubmissionDate);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}

