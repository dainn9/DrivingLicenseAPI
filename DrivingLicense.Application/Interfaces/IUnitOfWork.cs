
using DrivingLicense.Domain.Interfaces;

namespace DrivingLicense.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILicenseTypeRepository LicenseTypes { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}