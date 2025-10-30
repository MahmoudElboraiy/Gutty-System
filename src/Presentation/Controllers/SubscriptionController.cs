using Application.Subscriptions.Commands.FreezeSubscription;
using Application.Subscriptions.Commands.PlaceOrder;
using Application.Subscriptions.Query.GetPlanType;
using Application.Subscriptions.Query.GetSubscriptionDetails;
using Application.Subscriptions.Query.GetSubscriptionStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("FreezeSubscription")]
        public async Task<IActionResult> FreezeSubscription()
        {
            var result = await _mediator.Send(new FreezeSubscriptionCommand());
            return Ok(result);
        }

        [HttpGet("GetSubscriptionStatus")]
        public async Task<IActionResult> GetSubscriptionStatus()
        {
            var result = await _mediator.Send(new GetSubscriptionStatusQuery());
            return Ok(result);
        }
        [HttpGet("GetPlanType")]
        public async Task<IActionResult> GetPlanType()
        {
            var result = await _mediator.Send(new GetPlanTypeQuery());
            return Ok(result);
        }

        [HttpPost("PlaceSubscription")]
        public async Task<IActionResult> PlaceSubscription([FromBody] PlaceSubscriptionRequest request)
        {
            var userId = HttpContext.User.Identity.Name;
            // userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("UserId") ?? string.Empty;
            var command = new PlaceSubscriptionCommand(
                UserId: userId,
                PlanId: request.PlanId,
                DaysLeft: request.DaysLeft,
                LunchMealsLeft: request.LunchMealsLeft,
                CarbGrams: request.CarbGrams,
                StartDate: request.StartDate,
                IsCurrent: request.IsCurrent,
                IsPaused: request.IsPaused,
                LunchCategories: request.LunchCategories?.Select(c => new PlaceSubscriptionPlanCategory(c.NumberOfMeals, c.NumberOfMealsLeft, c.ProteinGrams, c.PricePerGram, c.AllowProteinChange, c.MaxProteinGrams, c.SubCategoryId)).ToList() ?? new(),
                PromoCodeId: request.PromoCodeId
            );
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetSubscriptionDetails")]
        public async Task<IActionResult> GetSubscriptionDetails()
        {
            var result = await _mediator.Send(new GetSubscriptionDetailsQuery());
            return Ok(result);
        }
    }
}
