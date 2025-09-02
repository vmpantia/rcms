using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCMS.Application.Courses.Commands;
using RCMS.Application.Courses.Queries;
using RCMS.Shared.Enumerations;
using RCMS.Shared.Models.Courses;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetCoursesAsync() => await SendRequestAsync(new GetCoursesQuery());
    
    [HttpPost, Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> CreateCourseAsync([FromBody] SaveCourseDto request) => await SendRequestAsync(new CreateCourseCommand(request));
    
    [HttpGet("Categories"), Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> GetCourseCategoriesAsync() => await SendRequestAsync(new GetCourseCategoriesQuery());
}