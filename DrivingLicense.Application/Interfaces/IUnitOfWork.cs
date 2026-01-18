
using DrivingLicense.Domain.Interfaces;

namespace DrivingLicense.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILicenseTypeRepository LicenseTypes { get; }
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        IRegisterFileRepository RegisterFile { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}