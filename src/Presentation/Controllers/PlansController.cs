using Application.Plans.Commands.CreatePlan;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePlanAdmin([FromBody] CreatePlanCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match(
            response => Ok(response),
            errors => Problem(errors.First().Description)
        );
    }
}
