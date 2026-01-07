
using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.Course;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Application.Mappers;

namespace DrivingLicense.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;

        public CourseService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CourseDto> CreateAsync(CourseCreateDto dto)
        {
            var licenseTypeExists = await _uow.LicenseTypes.ExistsByIdAsync(dto.LicenseTypeId);

            if (!licenseTypeExists)
                throw new NotFoundException("License Type not found.");

            var exists = await _uow.Courses.ExistsByNameAsync(dto.CourseName);

            if (exists)
                throw new ConflictException("Course with the same name already exists.");

            var entity = dto.ToEntity();
            _uow.Courses.Add(entity);
            await _uow.CommitAsync();
            return entity.ToDto();
        }

        public async Task<PagedResult<CourseDto>> GetPageAsync(PaginationParams pageParams)
        {
            pageParams.Normalize();

            var (entities, totalCount) = await _uow.Courses.GetPageAsync(pageParams.PageNumber, pageParams.PageSize);
            var dtos = entities.Select(e => e.ToDto());

            var pagedResult = new PagedResult<CourseDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pageParams.PageNumber,
                PageSize = pageParams.PageSize
            };

            if (totalCount > 0 && pagedResult.PageNumber > pagedResult.TotalPages)
                throw new BadRequestException($"Page number {pageParams.PageNumber} exceeds total pages {pagedResult.TotalPages}.");

            return pagedResult;
        }

        public async Task<CourseDto> GetByIdAsync(Guid id)
        {
            var entity = await _uow.Courses.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("Course not found.");

            return entity.ToDto();
        }

        public async Task UpdateAsync(Guid id, CourseUpdateDto dto)
        {
            var exists = await _uow.Courses.FindAsync(id);
            if (exists == null)
                throw new NotFoundException("Course not found.");

            var nameExists = await _uow.Courses.ExistsByNameAsync(name: dto.CourseName, excludeId: id);
            if (nameExists)
                throw new ConflictException("Course with the same name already exists.");

            var licenseTypeExists = await _uow.LicenseTypes.ExistsByIdAsync(dto.LicenseTypeId);

            if (!licenseTypeExists)
                throw new NotFoundException("License Type not found.");

            exists.MapFromUpdateDto(dto);
            _uow.Courses.Update(exists);
            await _uow.CommitAsync();
        }
    }
}