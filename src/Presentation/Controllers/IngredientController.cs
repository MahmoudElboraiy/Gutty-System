
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

    //[HttpGet]
    //public async Task<IActionResult> GetIngredients([FromQuery] GetIngredientsQuery query)
    //{
    //    var result = await _mediator.Send(query);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}

    //[HttpPost]
    //public async Task<IActionResult> UpdateIngredient([FromBody] CreateIngredientCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}

    //[HttpPut]
    //public async Task<IActionResult> UpdateIngredient([FromBody] UpdateIngredientCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}

    //[HttpDelete("{id:int}")]
    //public async Task<IActionResult> DeleteIngredient([FromRoute] int id)
    //{
    //    var command = new DeleteIngredientCommand(id);
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}

    //[HttpGet("change")]
    //public async Task<IActionResult> GetChange([FromQuery] GetIngredientsChangesQuery query)
    //{
    //    var result = await _mediator.Send(query);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}
}
