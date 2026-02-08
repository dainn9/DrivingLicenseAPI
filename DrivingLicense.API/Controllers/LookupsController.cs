using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.DTOs.Common;
using DrivingLicense.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILicenseTypeService _licenseTypeService;
        private readonly ICourseService _courseService;

        public LookupsController(
            ILicenseTypeService licenseTypeService,
            ICourseService courseService
           )
        {
            _licenseTypeService = licenseTypeService;
            _courseService = courseService;
        }

        [HttpGet("license-types")]
        public async Task<IActionResult> GetLicenseTypes()
        {
            var licenseTypes = await _licenseTypeService.GetDropDownListAsync();
            return Ok(ApiResponse<IEnumerable<LookupDto>>.SuccessResponse(licenseTypes));
        }
    }
}
