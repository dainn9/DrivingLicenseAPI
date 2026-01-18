using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using DrivingLicense.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DrivingLicense.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(DrivingDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, Guid? excludeId)
            => await _context.Students.AsNoTracking().AnyAsync(s => s.PhoneNumber == phoneNumber && (excludeId == null || s.Id != excludeId));

        public async Task<bool> ExistsByIdentityCardAsync(string identityCard, Guid? excludeId)
            => await _context.Students.AsNoTracking().AnyAsync(s => s.IdentityCard == identityCard && (excludeId == null || s.Id != excludeId));

        public async Task<bool> ExistsByEmailAsync(string email, Guid? excludeId)
            => await _context.Students.AsNoTracking().AnyAsync(s => s.Email == email && (excludeId == null || s.Id != excludeId));

        public async Task<(List<Student>, int)> GetPageAsync(int pageNumber, int pageSize)
        {
            var query = _context.Students.AsNoTracking().OrderBy(s => s.Id);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
            => await _context.Students.AsNoTracking().AnyAsync(s => s.Id == id);
    }
}