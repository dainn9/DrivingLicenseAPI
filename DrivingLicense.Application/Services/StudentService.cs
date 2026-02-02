using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.Student;
using DrivingLicense.Application.Helpers;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Application.Mappers;

namespace DrivingLicense.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;

        public StudentService(IUnitOfWork uow) => _uow = uow;

        public async Task<StudentDto> CreateAsync(StudentCreateDto dto)
        {
            if (!ValidationHelper.IsValidCCCD(dto.IdentityCard))
                throw new BadRequestException("Invalid Identitycard");

            if (!ValidationHelper.IsValidBirthDate(dto.DateOfBirth, 18))
                throw new BadRequestException("Student must be at least 18 years old");

            var identityCardExists = await _uow.Students.ExistsByIdentityCardAsync(dto.IdentityCard);

            if (identityCardExists)
                throw new ConflictException("Identity Card already exists.");

            var phoneExists = await _uow.Students.ExistsByPhoneNumberAsync(dto.PhoneNumber);

            if (phoneExists)
                throw new ConflictException("Phone number already exists.");

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailExists = await _uow.Students.ExistsByEmailAsync(dto.Email);

                if (emailExists)
                    throw new ConflictException("Email already exists.");
            }

            var entity = dto.ToEntity();
            _uow.Students.Add(entity);
            await _uow.CommitAsync();
            return entity.ToDto();
        }

        public async Task<PagedResult<StudentDto>> GetPageAsync(PaginationParams pageParams, string? searchTerm = null)
        {
            pageParams.Normalize();

            var (entities, totalCount) = await _uow.Students.GetPageAsync(pageParams.PageNumber, pageParams.PageSize, searchTerm);

            if (totalCount > 0)
            {
                var totalPages = (int)Math.Ceiling(
                    totalCount / (double)pageParams.PageSize);

                if (pageParams.PageNumber > totalPages)
                    throw new BadRequestException(
                        $"Page number {pageParams.PageNumber} exceeds total pages {totalPages}.");
            }

            var dtos = entities.Select(e => e.ToDto()).ToList();

            var pagedResult = new PagedResult<StudentDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pageParams.PageNumber,
                PageSize = pageParams.PageSize
            };

            return pagedResult;
        }

        public async Task<StudentDto> GetByIdAsync(Guid id)
        {
            var entity = await _uow.Students.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException("Student not found.");

            return entity.ToDto();
        }

        public async Task UpdateAsync(Guid id, StudentUpdateDto dto)
        {
            var exists = await _uow.Students.FindAsync(id);
            if (exists == null)
                throw new NotFoundException("Student not found.");

            if (!ValidationHelper.IsValidCCCD(dto.IdentityCard))
                throw new BadRequestException("Invalid Identitycard");

            if (!ValidationHelper.IsValidBirthDate(dto.DateOfBirth, 18))
                throw new BadRequestException("Student must be at least 18 years old");

            var identityCardExists = await _uow.Students.ExistsByIdentityCardAsync(dto.IdentityCard, excludeId: id);

            if (identityCardExists)
                throw new ConflictException("Identity Card already exists.");

            var phoneExists = await _uow.Students.ExistsByPhoneNumberAsync(dto.PhoneNumber, excludeId: id);

            if (phoneExists)
                throw new ConflictException("Phone number already exists.");

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailExists = await _uow.Students.ExistsByEmailAsync(dto.Email, excludeId: id);

                if (emailExists)
                    throw new ConflictException("Email already exists.");
            }

            exists.MapFromUpdateDto(dto);
            _uow.Students.Update(exists);
            await _uow.CommitAsync();
        }
    }
}