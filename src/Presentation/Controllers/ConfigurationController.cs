
using Application.Configuration.Command.UpdateSystemConfiguration;
using Application.Configuration.Query.GetSystemConfiguration;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetConfiguration()
        {
            var result = await _mediator.Send(new GetSystemConfigurationQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateConfiguration([FromBody] UpdateSystemConfigurationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
