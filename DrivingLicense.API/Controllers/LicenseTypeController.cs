using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.DTOs.LicenseType;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenseTypeController : ControllerBase
    {
        private readonly ILicenseTypeService _licenseTypeService;

        public LicenseTypeController(ILicenseTypeService licenseTypeService)
        {
            _licenseTypeService = licenseTypeService;
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LicenseTypeCreateDto dto)
        {
            var result = await _licenseTypeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<LicenseTypeDto>.SuccessResponse(result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var licenseType = await _licenseTypeService.GetByIdAsync(id);
            return Ok(ApiResponse<LicenseTypeDto>.SuccessResponse(licenseType));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var licenseTypes = await _licenseTypeService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LicenseTypeDto>>.SuccessResponse(licenseTypes));

        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LicenseTypeUpdateDto dto)
        {
            await _licenseTypeService.UpdateAsync(id, dto);
            return NoContent();
        }

    }
}