using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Agents.Commands;
using RealEstateApp.Application.Features.User.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetPagedUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPagedUsersQuery(page, pageSize));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result =  await _mediator.Send(new GetUserByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateAgentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(GetUserById), new { id = result.Value }, result.Value) : BadRequest(result.Errors);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateAgentCommand command, CancellationToken cancellation)
        {
            if (id != command.AgentId)
                return BadRequest("Mismatched user ID.");

            var result = await _mediator.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

    }
}
