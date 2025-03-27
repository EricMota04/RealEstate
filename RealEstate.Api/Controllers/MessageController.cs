using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Chat.Querys;


namespace RealEstate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Client,Agent")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("by-conversation")]
        public async Task<IActionResult> GetByConversation([FromQuery] Guid conversationId)
        {
            if (conversationId == Guid.Empty)
                return BadRequest("ConversationId is required.");

            var result = await _mediator.Send(new GetMessagesByConversationIdQuery(conversationId));

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}


