using Application.Categories.Queries;
using Application.Meals.Command.CreateCategory;
using Application.Meals.Command.CreateMeal;
using Application.Meals.Command.CreateSubCategory;
using Application.Meals.Command.DeleteCategory;
using Application.Meals.Command.DeleteMeal;
using Application.Meals.Command.DeleteSubCategory;
using Application.Meals.Command.UpdateCategory;
using Application.Meals.Command.UpdateMeal;
using Application.Meals.Command.UpdateSubCategory;
using Application.Meals.Query.GetAllMeals;
using Application.Meals.Query.GetAllMealsWithSearch;
using Application.Meals.Query.GetCategoryDetails;
using Application.Meals.Query.GetMealDetails;
using Application.Meals.Query.GetMeals;
using Application.Meals.Query.GetSubCategoriesById;
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
        [HttpGet("GetSubCategories/{id}")]
        public async Task<IActionResult> GetSubCategories([FromRoute] int id)
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
        [HttpGet("GetAllMealsWithSearch")]
        public async Task<IActionResult> GetAllMealsWithSearch([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchName = null,
            [FromQuery] string? SubCategoryName = null)
        {
            var result = await _mediator.Send(new GetAllMealsWithSearchQuery(pageNumber, pageSize, searchName, SubCategoryName));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetMeals/{id}")]
        public async Task<IActionResult> GetMeals([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetMealsQuery(id));
            return Ok(result);
        }
        //[Authorize]
        [HttpGet("GetMealDetails/{id}")]
        public async Task<IActionResult> GetMealDetails([FromRoute] int id)
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
        [HttpPost("CreateMeal"),Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> CreateMeal([FromForm] CreateMealCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPut("UpdateMeal"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> UpdateMeal([FromForm]UpdateMealCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeleteMeal/{id}"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> DeleteMeal([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteMealCommand(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetCategoryDetails/{id}")]
        public async Task<IActionResult> GetCategoryDetails([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetCategoryDetailsQuery(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("CreateCategory"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPut("EditCategory"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> EditCategory(UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeleteCategory/{id}"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetSubCategoriesById/({id})")]
        public async Task<IActionResult> GetSubCategoriesById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSubCategoriesByIdQuery(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("CreateSubCategory"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> CreateSubCategory([FromForm]CreateSubCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);

        }
        [HttpPut("UpdateSubCategory"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> UpdateSubCategory([FromForm]UpdateSubCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeleteSubCategory/{id}"), Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSubCategoryCommand(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }

    }
}
