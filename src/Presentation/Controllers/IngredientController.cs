
using Application.Ingredients.Commands.CreateIngredient;
using Application.Ingredients.Commands.DeleteIngredient;
using Application.Ingredients.Commands.UpdateIngredient;
using Application.Ingredients.Queries.GetIngredientById;
using Application.Ingredients.Queries.GetIngredients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngredientController : Controller
{
    private readonly ISender _mediator;

    public IngredientController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetIngredient")]
    public async Task<IActionResult> GetIngredients([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetIngredientsQuery(pageNumber, pageSize));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpGet("GetIngredientById/{id:int}")]
    public async Task<IActionResult> GetIngredientById([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetIngredientByIdQuery(id));
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("CreateIngredient")]
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateIngredient([FromBody] UpdateIngredientCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteIngredient([FromRoute] int id)
    {
        var command = new DeleteIngredientCommand(id);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
