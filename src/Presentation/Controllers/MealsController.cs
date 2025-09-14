//using Application.Meals.Commands.CreateMeal;
//using Application.Meals.Commands.DeleteMeal;
using Application.Meals.Commands.Create;
using Application.Meals.Commands.DeleteMeal;
using Application.Meals.Commands.UpdateMeal;
using Application.Meals.Queries.GetMeal;
//using Application.Meals.Queries.GetMeals;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class MealsController : ControllerBase
{
    private readonly ISender _mediator;

    public MealsController(ISender mediator)
    {
        _mediator = mediator;
    }

    //[HttpGet]
    //public async Task<IActionResult> GetMeals([FromQuery] GetMealsQuery query)
    //{
    //    var result = await _mediator.Send(query);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMeal([FromRoute] Guid id)
    {
        var query = new GetMealQuery(id);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [HttpPost]
    //[Authorize] // Add appropriate authorization as needed
    //public async Task<IActionResult> CreateMeal([FromBody] CreateMealCommand command)
    //{
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(
    //        success => CreatedAtAction(nameof(GetMeal), new { id = success.Id }, success),
    //        BadRequest);
    //}

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateMeal([FromBody] UpdateMealCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    //[HttpDelete("{id:guid}")]
    //[Authorize]
    //public async Task<IActionResult> DeleteMeal([FromRoute] Guid id)
    //{
    //    var command = new DeleteMealCommand(id);
    //    var result = await _mediator.Send(command);
    //    return result.Match<IActionResult>(Ok, BadRequest);
    //}

    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> CreateMeal([FromBody] CreateMealCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(GetMeal), new { id = success.Id }, success),
            BadRequest);
    }

    [HttpDelete ("{id:guid}")]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> DeleteMeal([FromRoute] Guid id)
    {
        var command = new DeleteMealCommand(id);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
