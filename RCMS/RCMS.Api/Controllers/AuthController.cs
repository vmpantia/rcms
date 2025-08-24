using MediatR;
using Microsoft.AspNetCore.Mvc;
using RCMS.Application.Users.Commands;
using RCMS.Shared.Models.Users;

namespace RCMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto dto) => await SendRequestAsync(new LoginUserCommand(dto));
}