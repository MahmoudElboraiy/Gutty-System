using Application.Plans.Commands.CreatePlan;
using Application.Plans.Queries.GetPlans;
using Application.Plans.Queries.NewFolder1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using System.Security.Claims;

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

    [HttpGet]
    public async Task<IActionResult> GetPlans()
    {
        var result = await _mediator.Send(new GetPlansQuery());
        return Ok(result);
    }

    [HttpPost("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePlanAdmin([FromBody] CreatePlanCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{id}/calculate")]
    public async Task<IActionResult> Calculate(Guid id, [FromBody] CalculatePlanPriceRequest request)
    {
        var query = new CalculatePlanPriceQuery(
            PlanId: id,
            RiceCarbGrams: request.RiceCarbGrams,
            PastaCarbGrams: request.PastaCarbGrams,
            PromoCode: request.PromoCode,
            UserId: User.FindFirstValue("UserId") ?? string.Empty,
            Categories: request.Categories?
                .Select(c => new CategoryModificationDto(c.CategoryId, c.NumberOfMeals, c.ProteinGrams))
                .ToList()
        );

        var response = await _mediator.Send(query);
        return Ok(response);
    }
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok("Test very successful");
    }
}
