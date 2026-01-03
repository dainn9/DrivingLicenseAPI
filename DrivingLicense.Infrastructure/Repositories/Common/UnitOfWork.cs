using DrivingLicense.Application.Interfaces;
using DrivingLicense.Domain.Interfaces;
using DrivingLicense.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace DrivingLicense.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DrivingDbContext _context;
        private IDbContextTransaction? _transaction;
        public ILicenseTypeRepository LicenseTypes { get; }

        public ICourseRepository Courses { get; }

        public UnitOfWork(DrivingDbContext context, ILicenseTypeRepository licenseTypeRepo, ICourseRepository courseRepo)
        {
            _context = context;
            LicenseTypes = licenseTypeRepo;
            Courses = courseRepo;

        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null) return;
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }

        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No transaction to rollback.");

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}