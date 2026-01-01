using DrivingLicense.Domain.Entities;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using DrivingLicense.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DrivingLicense.Infrastructure.Repositories
{
    public class LicenseTypeRepository : GenericRepository<LicenseType>, ILicenseTypeRepository
    {
        public LicenseTypeRepository(DrivingDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null)
        {
            var normalized = name.Trim().ToUpper();
            return await _context.LicenseTypes.AsNoTracking().AnyAsync(lt => lt.LicenseTypeName.ToUpper() == normalized && (excludeId == null || lt.Id != excludeId));
        }
    }
}