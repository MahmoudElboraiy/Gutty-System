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
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("PerUser")]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlans([FromQuery] int pageNumber =1 ,int pageSize =5)
    {
        var result = await _mediator.Send(new GetPlansQuery(pageNumber ,pageSize));
        return Ok(result);
    }
    [HttpGet("GetPlanById/{id:guid}")]
    public async Task<IActionResult> GetPlanById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetPlanByIdQuery(id));
        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost("CreatePlan"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
    public async Task<IActionResult> CreatePlanAdmin([FromForm] CreatePlanCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("calculatePrice")]
    public async Task<IActionResult> Calculate( [FromBody] CalculatePlanPriceQuery query)
    {   
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpDelete("DeletePlan/{id}"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        var result = await _mediator.Send(new DeletePlanCommand(id));
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPut("EditPlan"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
    public async Task<IActionResult> EditPlan( [FromForm] EditPlanCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }

}
