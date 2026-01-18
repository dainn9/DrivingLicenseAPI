using DrivingLicense.Domain.Entities;

namespace DrivingLicense.Domain.Interfaces
{
    public interface IRegisterFileRepository : IGenericRepository<RegisterFile>
    {
        Task<(List<RegisterFile>, int)> GetPageAsync(int pageNumber, int pageSize);
        Task<int> CountByCourseIdAsync(Guid courseId, Guid? excludeId = null);
        Task<RegisterFile?> GetDetailByIdAsync(Guid id);
    }
}
