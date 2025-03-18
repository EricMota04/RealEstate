using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Agents.Commands;
using RealEstateApp.Application.Features.Agents.Queries;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AgentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtener todos los agentes paginados
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPagedAgents([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPagedAgentsQuery(page, pageSize));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        /// <summary>
        /// Obtener un agente por su ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgentById(Guid id)
        {
            var result = await _mediator.Send(new GetAgentByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        /// <summary>
        /// Crear un nuevo agente (Solo Administradores)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")] // Solo los administradores pueden crear agentes
        public async Task<IActionResult> CreateAgent([FromBody] CreateAgentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(GetAgentById), new { id = result.Value }, result.Value) : BadRequest(result.Errors);
        }

        /// <summary>
        /// Actualizar el estado de un agente (Solo Administradores)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAgentStatus(Guid id, [FromBody] UpdateAgentCommand command, CancellationToken cancellationToken)
        {
            if (id != command.AgentId)
                return BadRequest("Mismatched agent ID.");

            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
        }
    }
}
