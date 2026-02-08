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
    public class LicenseTypesController : ControllerBase
    {
        private readonly ILicenseTypeService _licenseTypeService;

        public LicenseTypesController(ILicenseTypeService licenseTypeService)
        {
            _licenseTypeService = licenseTypeService;
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LicenseTypeCreateDto dto)
        {
            var result = await _licenseTypeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { licenseTypeId = result.Id }, ApiResponse<LicenseTypeDto>.SuccessResponse(result));
        }

        [HttpGet("{licenseTypeId:guid}")]
        public async Task<IActionResult> GetById(Guid licenseTypeId)
        {
            var licenseType = await _licenseTypeService.GetByIdAsync(licenseTypeId);
            return Ok(ApiResponse<LicenseTypeDto>.SuccessResponse(licenseType));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var licenseTypes = await _licenseTypeService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LicenseTypeDto>>.SuccessResponse(licenseTypes));

        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropDownList()
        {
            var licenseTypes = await _licenseTypeService.GetDropDownListAsync();
            return Ok(ApiResponse<IEnumerable<LicenseTypeDropDownDto>>.SuccessResponse(licenseTypes));

        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPut("{licenseTypeId:guid}")]
        public async Task<IActionResult> Update(Guid licenseTypeId, [FromBody] LicenseTypeUpdateDto dto)
        {
            await _licenseTypeService.UpdateAsync(licenseTypeId, dto);
            return NoContent();
        }

    }
}