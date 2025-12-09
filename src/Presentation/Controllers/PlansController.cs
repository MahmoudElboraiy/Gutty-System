using Application.Plans.Commands.CreatePlan;
using Application.Plans.Queries.GetPlans;
using Application.Plans.Queries.NewFolder1;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;
using System.Security.Claims;
using Infrastructure.Data;
using Application.Subscriptions.Commands.PlaceOrder;
using Domain.Models.Entities;
using Vonage.Video.Authentication;
using Domain.Enums;
using Application.Authentication.Commands.Otp.SendOtp;
using System.Numerics;
using Application.Plans.Commands.DeletePlan;
using Application.Plans.Commands.EditPlan;
using Application.Plans.Queries.GetPlanById;

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
    [HttpGet("GetPlanById/{id:guid}")]
    public async Task<IActionResult> GetPlanById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetPlanByIdQuery(id));
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("CreatePlan")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreatePlanAdmin([FromBody] CreatePlanCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("calculatePrice")]
    public async Task<IActionResult> Calculate( [FromBody] CalculatePlanPriceQuery query)
    {
        //var query = new CalculatePlanPriceQuery(
        //    PlanId: id,
        //    CarbGrams: request.CarbGrams,
        //    PromoCode: request.PromoCode,
        //    UserId: User.FindFirstValue("UserId") ?? string.Empty,
        //    Categories: request.Categories?
        //        .Select(c => new CategoryModificationDto(c.CategoryId, c.NumberOfMeals, c.ProteinGrams))
        //        .ToList()
        //);

        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpDelete("DeletePlan{id}"),Authorize(Roles = "Admin,CustomerService")]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        var result = await _mediator.Send(new DeletePlanCommand(id));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPut("EditPlan"), Authorize(Roles = "Admin,CustomerService")]
    public async Task<IActionResult> EditPlan( [FromBody] EditPlanCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

}
