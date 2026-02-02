using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<(List<Student>, int)> GetPageAsync(int pageNumber, int pageSize, string? searchTerm = null);
        Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, Guid? excludeId = null);
        Task<bool> ExistsByIdentityCardAsync(string identityCard, Guid? excludeId = null);
        Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null);
        Task<bool> ExistsByIdAsync(Guid id);
    }
}