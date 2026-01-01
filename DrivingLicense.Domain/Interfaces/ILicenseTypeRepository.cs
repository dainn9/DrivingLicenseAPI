using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Domain.Interfaces
{
    public interface ILicenseTypeRepository : IGenericRepository<LicenseType>
    {
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
    }
}