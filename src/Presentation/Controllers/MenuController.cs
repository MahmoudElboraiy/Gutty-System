using Application.Categories.Queries;
using Application.Meals.Command.CreateMeal;
using Application.Meals.Command.DeleteMeal;
using Application.Meals.Command.UpdateMeal;
using Application.Meals.Query.GetAllMeals;
using Application.Meals.Query.GetMealDetails;
using Application.Meals.Query.GetMeals;
using Application.Plans.Queries.GetPlans;
using Application.SubCategories.Query.GetBreakFastAndDinner;
using Application.SubCategories.Query.GetSubCategories;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.Send(new GetCategoriesQuery());
            return Ok(result);
        }
        [HttpGet("GetSubCategories{id}")]
        public async Task<IActionResult> GetSubCategories(int id)
        {
            var result = await _mediator.Send(new GetSubCategoriesQuery(id));
            return Ok(result);
        }
        [HttpGet("GetAllMeals")]
        public async Task<IActionResult> GetAllMeals([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAllMealsQuery(pageNumber, pageSize));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetMeals{id}")]
        public async Task<IActionResult> GetMeals(int id)
        {
            var result = await _mediator.Send(new GetMealsQuery(id));
            return Ok(result);
        }
        //[Authorize]
        [HttpGet("GetMealDetails{id}")]
        public async Task<IActionResult> GetMealDetails(int id)
        {
            var identity = HttpContext.User.Identity.Name;
            var result = await _mediator.Send(new GetMealDetailsQuery(id, identity));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetSubCategoryByType")]
        public async Task<IActionResult> GetSubCategoryByType([FromQuery] MealType type)
        {
            var result = await _mediator.Send(new GetSubCategoriesByTypeQuery(type));
            return Ok(result);
        }
        [HttpPost("CreateMeal")]
        public async Task<IActionResult> CreateMeal(CreateMealCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPut("UpdateMeal")]
        public async Task<IActionResult> UpdateMeal(UpdateMealCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeleteMeal{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var result = await _mediator.Send(new DeleteMealCommand(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }

    }
}
