using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.RegisterFile;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Application.Mappers;
using DrivingLicense.Application.Validators;
using DrivingLicense.Domain.Enums;

namespace DrivingLicense.Application.Services
{
    public class RegisterFileService : IRegisterFileService
    {
        private readonly IUnitOfWork _uow;

        public RegisterFileService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<RegisterFileDto> CreateAsync(RegisterFileCreateDto dto)
        {
            var studentExists = await _uow.Students.ExistsByIdAsync(dto.StudentId);
            if (!studentExists)
                throw new NotFoundException("Student not found.");

            var licenseTypeExists = await _uow.LicenseTypes.ExistsByIdAsync(dto.LicenseTypeId);
            if (!licenseTypeExists)
                throw new NotFoundException("License type not found.");

            var entity = dto.ToEntity();
            _uow.RegisterFile.Add(entity);
            await _uow.CommitAsync();
            return entity.ToDto();
        }

        public async Task<RegisterFileDto> GetByIdAsync(Guid id)
        {
            var entity = await _uow.RegisterFile.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("Register file not found.");

            return entity.ToDto();
        }

        public async Task<RegisterFileDetailDto> GetDetailByIdAsync(Guid id)
        {
            var entity = await _uow.RegisterFile.GetDetailByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("Register File not found.");

            return entity.ToDetailDto();
        }

        public async Task<PagedResult<RegisterFileDto>> GetPageAsync(PaginationParams pageParams)
        {
            pageParams.Normalize();

            var (entities, totalCount) = await _uow.RegisterFile.GetPageAsync(pageParams.PageNumber, pageParams.PageSize);
            var dtos = entities.Select(e => e.ToDto());

            var pagedResult = new PagedResult<RegisterFileDto>
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

        public async Task UnassignCourseAsync(Guid id)
        {
            var entity = await _uow.RegisterFile.FindAsync(id);
            if (entity == null)
                throw new NotFoundException("Register file not found.");

            if (entity.Status != RegisterFileStatus.Approved)
                throw new ConflictException("Only approved register files can unassign course.");

            if (!entity.CourseId.HasValue)
                throw new ConflictException("Register file is not assigned to any course.");

            entity.CourseId = null;
            _uow.RegisterFile.Update(entity);
            await _uow.CommitAsync();
        }

        public async Task AssignCourseAsync(Guid id, RegisterFileUpdateCourseDto dto)
        {
            var entity = await _uow.RegisterFile.FindAsync(id);
            if (entity == null)
                throw new NotFoundException("Register file not found.");

            if (entity.Status != RegisterFileStatus.Approved)
                throw new ConflictException("Only approved register files can be assigned to a course.");

            var course = await _uow.Courses.GetByIdAsync(dto.CourseId);
            if (course == null)
                throw new NotFoundException("Course not found.");

            if (course.LicenseTypeId != entity.LicenseTypeId)
                throw new ConflictException("License type does not belong to this course.");

            var registeredCount = await _uow.RegisterFile.CountByCourseIdAsync(dto.CourseId, id);

            if (registeredCount >= course.Capacity)
                throw new ConflictException("Class is already full.");

            if (entity.CourseId == dto.CourseId)
                return;

            entity.CourseId = dto.CourseId;
            _uow.RegisterFile.Update(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateLicenseTypeAsync(Guid id, RegisterFileUpdateLicenseTypeDto dto)
        {

            var entity = await _uow.RegisterFile.FindAsync(id);
            if (entity == null)
                throw new NotFoundException("Register file not found.");

            if (entity.Status != RegisterFileStatus.Created)
                throw new ConflictException("License type can only be changed when the register file is in Created status.");

            var licenseTypeExists = await _uow.LicenseTypes.ExistsByIdAsync(dto.LicenseTypeId);
            if (!licenseTypeExists)
                throw new NotFoundException("License type not found.");

            if (entity.LicenseTypeId == dto.LicenseTypeId)
                return;

            entity.LicenseTypeId = dto.LicenseTypeId;
            _uow.RegisterFile.Update(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateStatusAsync(Guid id, RegisterFileUpdateStatusDto dto)
        {
            var entity = await _uow.RegisterFile.FindAsync(id);
            if (entity == null)
                throw new NotFoundException("Register file not found.");

            if (dto.Status == RegisterFileStatus.InProgress)
            {
                if (entity.CourseId == null)
                    throw new ConflictException("Register file has not been assigned to a course.");

                if (entity.Course!.StartDate > DateTime.UtcNow)
                    throw new ConflictException("Course has not started yet.");
            }

            if (entity.Status == dto.Status)
                return;

            if (!RegisterFileValidator.IsValidStatusTransition(entity.Status, dto.Status))
                throw new ConflictException("Invalid status transition.");

            entity.Status = dto.Status;
            _uow.RegisterFile.Update(entity);
            await _uow.CommitAsync();
        }
    }
}
