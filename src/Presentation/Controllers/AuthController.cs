using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Queries.UserLogin;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginQuery query)
    {
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
