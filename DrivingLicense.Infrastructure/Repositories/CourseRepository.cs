
using DrivingLicense.Application.DTOs.Course;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Enums;
using DrivingLicense.Domain.Extensions;
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

        public async Task<bool> ExistsByIdAsync(Guid id)
            => await _context.Courses.AsNoTracking().AnyAsync(c => c.Id == id);

        public async Task<List<CourseDropDownDto>> GetCourseByLicenseTypeIdAsync(Guid licenseTypeId, CourseStatus courseStatus)
         => await _context.Courses.AsNoTracking().Where(c => c.LicenseTypeId == licenseTypeId && c.Status == courseStatus).Select(
             c => new CourseDropDownDto
             {
                 Id = c.Id,
                 CourseName = c.CourseName,
             }).ToListAsync();


        public async Task<CourseDto?> GetDetailAsync(Guid id)
        {
            var data = await _context.Courses.Where(c => c.Id == id).Select(c => new
            {
                Id = c.Id,
                c.CourseName,
                c.StartDate,
                c.EndDate,
                c.Capacity,
                Count = c.RegisterFiles.Count(),
                c.Status,
                c.LicenseTypeId,
                LicenseTypeName = c.LicenseType != null ? c.LicenseType.LicenseTypeName : string.Empty
            }).FirstOrDefaultAsync();

            if (data == null)
                return null;

            return new CourseDto
            {
                Id = data.Id,
                CourseName = data.CourseName,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Capacity = data.Capacity,
                CurrentStudentCount = data.Count,
                StatusCode = data.Status.ToCode(),
                StatusName = data.Status.ToText(),
                LicenseTypeId = data.LicenseTypeId,
                LicenseTypeName = data.LicenseTypeName,
            };
        }
    }
}