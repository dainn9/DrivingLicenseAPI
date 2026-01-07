using DrivingLicense.Application.Common.ApiResponses;
using DrivingLicense.Application.Common.Paging;
using DrivingLicense.Application.DTOs.Course;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DrivingLicense.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            var result = await _courseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { courseId = result.Id },
                ApiResponse<CourseDto>.SuccessResponse(result));
        }

        [HttpGet("{courseId:guid}")]
        public async Task<IActionResult> GetById(Guid courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId);
            return Ok(ApiResponse<CourseDto>.SuccessResponse(course));
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PaginationParams pageParams)
        {
            var courses = await _courseService.GetPageAsync(pageParams);
            return Ok(ApiResponse<PagedResult<CourseDto>>.SuccessResponse(courses));

        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPut("{courseId:guid}")]
        public async Task<IActionResult> Update(Guid courseId, [FromBody] CourseUpdateDto dto)
        {
            await _courseService.UpdateAsync(courseId, dto);
            return NoContent();
        }
    }
}