using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Appointments.Commands;
using RealEstateApp.Application.Features.Appointments.Querys;
using System.Security.Claims;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : throw new UnauthorizedAccessException("Invalid User ID");
        }

        private (Guid UserId, string Role) GetUserDetails()
        {
            var userId = GetUserId();
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? throw new UnauthorizedAccessException("User role not found");
            return (userId, role);
        }

        [HttpGet("client")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAppointmentsByClient([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0) return BadRequest("Page and pageSize must be greater than zero.");

            var result = await _mediator.Send(new GetAppointmentsByClientQuery(GetUserId(), page, pageSize));
            return result.IsSuccess ? (result.Value.TotalCount > 0 ? Ok(result.Value) : NoContent()) : BadRequest(result.Errors);
        }

        [HttpGet("client/active")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetActiveAppointmentsByClient([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0) return BadRequest("Page and pageSize must be greater than zero.");

            var result = await _mediator.Send(new GetActiveAppointmentsByClientQuery(GetUserId(), page, pageSize));
            return result.IsSuccess ? (result.Value.TotalCount > 0 ? Ok(result.Value) : NoContent()) : BadRequest(result.Errors);
        }

        [HttpGet("agent")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> GetAppointmentsByAgent([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0) return BadRequest("Page and pageSize must be greater than zero.");

            var result = await _mediator.Send(new GetAppointmentsByAgentQuery(GetUserId(), page, pageSize));
            return result.IsSuccess ? (result.Value.TotalCount > 0 ? Ok(result.Value) : NoContent()) : BadRequest(result.Errors);
        }

        [HttpPut("cancel/{appointmentId:guid}")]
        [Authorize(Roles = "Client,Agent")]
        public async Task<IActionResult> CancelAppointment(Guid appointmentId)
        {
            var (userId, role) = GetUserDetails();
            var command = new CancelAppointmentCommand(appointmentId)
            {
                ClientId = role == "Client" ? userId : null,
                AgentId = role == "Agent" ? userId : null
            };

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }

        [HttpPost]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var (userId, role) = GetUserDetails();
            if (role == "Agent") command.AgentId = userId;

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }

        [HttpGet("{appointmentId:guid}")]
        [Authorize(Roles = "Client,Agent")]
        public async Task<IActionResult> GetAppointmentDetails(Guid appointmentId)
        {
            var result = await _mediator.Send(new GetAppointmentDetailsQuery(appointmentId));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        [HttpPut("complete/{appointmentId:guid}")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> CompleteAppointment(Guid appointmentId)
        {
            var (userId, role) = GetUserDetails();
            var command = new CompleteAppointmentCommand(appointmentId) { AgentId = userId };

            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Errors);
        }
    }
}
