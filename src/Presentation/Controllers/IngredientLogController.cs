using Application.IngredientLogs.Queries.GetIngredientLogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientLogController : Controller
{
    private readonly ISender _mediator;

    public IngredientLogController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetIngredientLogs([FromQuery] GetIngredientLogsQuery query)
    {
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
}