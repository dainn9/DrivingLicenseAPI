using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.Student;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingLicense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;


        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            var result = await _studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { studentId = result.Id },
                ApiResponse<StudentDto>.SuccessResponse(result));
        }

        [HttpGet("{studentId:guid}")]
        public async Task<IActionResult> GetById(Guid studentId)
        {
            var student = await _studentService.GetByIdAsync(studentId);
            return Ok(ApiResponse<StudentDto>.SuccessResponse(student));
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationParams pageParams)
        {
            var students = await _studentService.GetPageAsync(pageParams);
            return Ok(ApiResponse<PagedResult<StudentDto>>.SuccessResponse(students));
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPut("{studentId:guid}")]
        public async Task<IActionResult> Update(Guid studentId, [FromBody] StudentUpdateDto dto)
        {
            await _studentService.UpdateAsync(studentId, dto);
            return NoContent();
        }
    }
}
