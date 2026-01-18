using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.RegisterFile;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterFilesController : ControllerBase
    {
        private readonly IRegisterFileService _registerFileService;

        public RegisterFilesController(IRegisterFileService registerFileService)
        {
            _registerFileService = registerFileService;
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterFileCreateDto dto)
        {
            var result = await _registerFileService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { registerFileId = result.Id },
                ApiResponse<RegisterFileDto>.SuccessResponse(result));
        }

        [HttpGet("{registerFileId:guid}")]
        public async Task<IActionResult> GetById(Guid registerFileId)
        {
            var registerFile = await _registerFileService.GetByIdAsync(registerFileId);
            return Ok(ApiResponse<RegisterFileDto>.SuccessResponse(registerFile));
        }

        [HttpGet("{registerFileId:guid}/detail")]
        public async Task<IActionResult> GetDetailById(Guid registerFileId)
        {
            var registerFile = await _registerFileService.GetDetailByIdAsync(registerFileId);
            return Ok(ApiResponse<RegisterFileDetailDto>.SuccessResponse(registerFile));
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationParams pageParams)
        {
            var registerFiles = await _registerFileService.GetPageAsync(pageParams);
            return Ok(ApiResponse<PagedResult<RegisterFileDto>>.SuccessResponse(registerFiles));

        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPatch("{registerFileId:guid}/unassign-course")]
        public async Task<IActionResult> UnassignCourse(Guid registerFileId)
        {
            await _registerFileService.UnassignCourseAsync(registerFileId);
            return NoContent();
        }


        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPatch("{registerFileId:guid}/assign-course")]
        public async Task<IActionResult> AssignCourse(Guid registerFileId, [FromBody] RegisterFileUpdateCourseDto dto)
        {
            await _registerFileService.AssignCourseAsync(registerFileId, dto);
            return NoContent();
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPatch("{registerFileId:guid}/license-type")]
        public async Task<IActionResult> UpdateLicenseType(Guid registerFileId, [FromBody] RegisterFileUpdateLicenseTypeDto dto)
        {
            await _registerFileService.UpdateLicenseTypeAsync(registerFileId, dto);
            return NoContent();
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPatch("{registerFileId:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid registerFileId, [FromBody] RegisterFileUpdateStatusDto dto)
        {
            await _registerFileService.UpdateStatusAsync(registerFileId, dto);
            return NoContent();
        }
    }
}
