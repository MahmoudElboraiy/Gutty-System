using Application.Inventory.Purchases.Command.CreateInventoryPurchase;
using Application.Inventory.Purchases.Command.DeleteInventoryPurchase;
using Application.Inventory.Purchases.Command.UpdateInventoryPurchase;
using Application.Inventory.Purchases.Query.GetPriceSummaryByDays;
using Application.Inventory.Purchases.Query.GetPurchaseOrderById;
using Application.Inventory.Purchases.Query.GetPurchasesByDays;
using Application.Inventory.Sales.Query.GetSaleOrderById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryPurchasesController : ControllerBase
    {
        private readonly ISender _mediator;
        public InventoryPurchasesController(ISender mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetPurchasesByDays")]
        public async Task<IActionResult> GetPurchases(
         [FromQuery] int days = 7,
         [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10)
        {
            var query = new GetPurchasesByDaysQuery(days, pageNumber, pageSize);
            var result = await _mediator.Send(query);
            return result.Match<IActionResult>(Ok, BadRequest);

        }
        [HttpGet("GetSalePurchaseById/{id:int}")]
        public async Task<IActionResult> GetPurchaseOrderById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetPurchaseOrderByIdQuery(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("CreateInventoryPurchase")]
        public async Task<IActionResult> CreateInventoryPurchase([FromBody] CreateInventoryPurchaseCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPut("UpdateInventoryPurchase")]
        public async Task<IActionResult> UpdateInventoryPurchase([FromBody] UpdateInventoryPurchaseCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeleteInventoryPurchase/{id:int}")]
        public async Task<IActionResult> DeleteInventoryPurchase([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteInventoryPurchaseCommand(id));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetPriceSummaryByDays")]
        public async Task<decimal> GetPriceSummaryByDays([FromQuery] int days = 7)
        {
            var result = await _mediator.Send(new GetPriceSummaryByDaysQuery(days));
            return result;
        }
    }
}
