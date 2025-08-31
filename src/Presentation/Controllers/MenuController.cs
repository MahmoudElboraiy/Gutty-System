using Application.Categories.Queries;
using Application.Meals.Query.GetMealDetails;
using Application.Meals.Query.GetMeals;
using Application.Plans.Queries.GetPlans;
using Application.SubCategories.Query.GetSubCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetMeals{id}")]
        public async Task<IActionResult> GetMeals(int id)
        {
            var result = await _mediator.Send(new GetMealsQuery(id));
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetMealDetails{id}")]
        public async Task<IActionResult> GetMealDetails(int id)
        {
            var identity = HttpContext.User.Identity.Name;
            var result = await _mediator.Send(new GetMealDetailsQuery(id, identity));
            return Ok(result);
        }
    }
}
