using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Chat.Querys;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client,Agent")]
    public class ConversationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConversationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{agentId}")]
        public async Task<IActionResult> GetByAgent(Guid agentId, int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetConversationsByAgentQuery(agentId, page, pageSize));
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClient(Guid clientId, int page = 1, int pageSize = 10)
        {
            var result = await _mediator.Send(new GetConversationsByClientQuery(clientId, page, pageSize));
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}
