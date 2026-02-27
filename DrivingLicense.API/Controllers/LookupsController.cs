using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.DTOs.Common;
using DrivingLicense.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILicenseTypeService _licenseTypeService;

        public LookupsController(
            ILicenseTypeService licenseTypeService
           )
        {
            _licenseTypeService = licenseTypeService;
        }

        [HttpGet("license-types")]
        public async Task<IActionResult> GetLicenseTypes()
        {
            var licenseTypes = await _licenseTypeService.GetDropDownListAsync();
            return Ok(ApiResponse<IEnumerable<LookupDto>>.SuccessResponse(licenseTypes));
        }
    }
}
