using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.PromoCodes.Query.GetPromoCodes;
using System.Text.RegularExpressions;
using Application.PromoCodes.Commands.CreatePromoCode;
using Application.PromoCodes.Query.GetPromoCodeByCode;
using Application.PromoCodes.Commands.EditPromoCode;
using Application.PromoCodes.Commands.DeletePromoCode;
namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PromoCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetPromocodes")]
        public async Task<IActionResult> GetPromocodes([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPromoCodesQuery(pageNumber,pageSize));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("GetPromocodeByCode/{code}")]
        public async Task<IActionResult> GetPromocodeByCode([FromRoute] string code)
        {
            var result = await _mediator.Send(new GetPromoCodeByCodeQuery(code));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("CreatePromoCode")]
        public async Task<IActionResult> CreatePromoCode([FromBody] CreatePromoCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPut("EditPromoCode")]
        public async Task<IActionResult> EditPromoCode([FromBody] EditPromoCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match<IActionResult>(Ok, BadRequest);
        }
        [HttpDelete("DeletePromoCode/{code}")]
        public async Task<IActionResult> DeletePromoCode([FromRoute] string code)
        {
            var result = await _mediator.Send(new DeletePromoCodeCommand(code));
            return result.Match<IActionResult>(Ok, BadRequest);
        }
    }
}
