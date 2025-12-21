
using Application.Inventory.Sales.Command.CreateInventorySales;
using Application.Inventory.Sales.Command.DeleteInventorySales;
using Application.Inventory.Sales.Command.UpdateInventorySales;
using Application.Inventory.Sales.Query.GetPriceSalesSummaryByDays;
using Application.Inventory.Sales.Query.GetSaleOrderById;
using Application.Inventory.Sales.Query.GetSalesByDays;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.CustomerService)}")]
    [EnableRateLimiting("PerUser")]
    public class InventorySalesController : ControllerBase
    {
        private readonly ISender _mediator;
        public InventorySalesController(ISender mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetSalesByDays")]
        public async Task<IActionResult> GetSales(
        [FromQuery] int days = 7,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchName =null)
        {
            var query = new GetSalesByDaysQuery(days, pageNumber, pageSize, searchName);
            var result = await _mediator.Send(query);
            return result.Match<IActionResult>(Ok, BadRequest);

        }
        [HttpGet("GetSaleOrderById/{id:int}")]
        public async Task<IActionResult> GetSaleOrderById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSaleOrderByIdQuery(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("CreateInventorySales")]
        public async Task<IActionResult> CreateInventorySales([FromBody] CreateInventorySalesCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPut("UpdateInventorySales")]
        public async Task<IActionResult> UpdateInventorySales([FromBody] UpdateInventorySalesCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeleteInventorySales/{id:int}")]
        public async Task<IActionResult> DeleteInventorySales([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteInventorySalesCommand(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetPriceSummaryByDays")]
        public async Task<IActionResult> GetPriceSalesSummaryByDays([FromQuery] int days = 7)
        {
            var result = await _mediator.Send(new GetPriceSalesSummaryByDaysQuery(days));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
    }
}
