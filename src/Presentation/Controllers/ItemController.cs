using Application.Items.Commands.CreateItem;
using Application.Items.Commands.UpdateItem;
using Application.Items.Queries.GetItem;
using Application.Items.Queries.GetItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : Controller
{
    private readonly ISender _mediator;

    public ItemController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetItem([FromRoute] Guid id)
    {
        var query = new GetItemQuery(id);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [HttpGet]
    public async Task<IActionResult> GetItems([FromQuery] GetItemsQuery query)
    {
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, NotFound);
    }
}
