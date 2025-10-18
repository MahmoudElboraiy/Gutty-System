using Application.Orders.Commands.AddCarbToMeal;
using Application.Orders.Commands.AddMeal;
using Application.Orders.Commands.ChooseDeliveryDate;
using Application.Orders.Commands.GetOrCreateOrder;
using Application.Orders.Commands.RemoveCarbFromMeal;
using Application.Orders.Commands.RemoveMeal;
using Application.Orders.Commands.SelectDays;
using Application.Orders.Query.CheckCompleteOrder;
using Application.Orders.Query.GetNumberOfRemainingMeals;
using Application.Orders.Query.GetNumberOfRemainingMealsInSubCategory;
using Application.Orders.Query.GetRemainingDays;
using Application.Orders.Query.ShowOrderDetails;
using Application.Orders.Commands.ConfirmOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetRemainingDays")]
        public async Task<IActionResult> GetRemainingDays()
        {
            var result = await _mediator.Send(new GetRemainingDaysQuery());
            return Ok(result);
        }
        [HttpPost("SelectDays{days}")]
        public async Task<IActionResult> PostSelectDays(uint days)
        {
            var result = await _mediator.Send(new SelectDaysCommand(days));
            return Ok(result);
        }
        [HttpGet("NumberOfRemainingMeals")]
        public async Task<IActionResult> GetNumberOfRemainingMeals()
        {
            var result = await _mediator.Send(new GetNumberOfRemainingMealsQuery());
            return Ok(result);
        }
        [HttpGet("RemainingMealsInSubCategory{subCategoryId}")]
        public async Task<IActionResult> GetNumberOfRemainingMealsInSubCategory(uint subCategoryId)
        {
            var result = await _mediator.Send(new GetNumberOfRemainingMealsInSubCategoryQuery(subCategoryId));   
            return Ok(result);
        }
        [HttpPost("CreateOrder{dayNumber}")]
        public async Task<IActionResult> CreateOrder(int dayNumber)
        {
            var result = await _mediator.Send(new CreateOrderCommand(dayNumber));
            return Ok(result);
        }
        [HttpPost("AddMeal")]
        public async Task<IActionResult> AddMeal([FromBody] AddMealCommand command)
        {
            var result =  await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("RemoveMeal")]
        public async Task<IActionResult> RemoveMeal([FromBody] RemoveMealCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("AddCarbToMeal")]
        public async Task<IActionResult> AddCarbToMeal([FromBody] AddCarbToMealCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("RemoveCarbFromMeal")]
        public async Task<IActionResult> RemoveCarbFromMeal([FromBody] RemoveCarbFromMealCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("ShowOrderDetails{orderId}")]
        public async Task<IActionResult> ShowOrderDetails(int orderId)
        {
            var result = await _mediator.Send(new ShowOrderDetailsQuery(orderId));
            return Ok(result);
        }
        [HttpGet("CheckCompleteOrder")]
        public async Task<IActionResult> CheckCompleteOrder()
        {
            var result = await _mediator.Send(new CheckCompleteOrderQuery());
            return Ok(result);
        }
        [HttpPost("ChooseDeliveryDate")]
        public async Task<IActionResult> ChooseDeliveryDate([FromBody]ChooseDeliveryDateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("ConfirmOrder")]
        public async Task<IActionResult> ConfirmOrder()
        {
            var result = await _mediator.Send(new ConfirmOrderCommand());
            return Ok(result);
        }
    }
}
