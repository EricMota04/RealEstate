using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Agents.Commands;
using RealEstateApp.Application.Features.Clients.Commands;
using RealEstateApp.Application.Features.Clients.Queries;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Obtiene todo los clientes paginados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedClients([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPagedClientsQuery(page, pageSize));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            var result = await _mediator.Send(new GetClientByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
        }

        /// <summary>
        /// Crear un nuevo cliente (Solo Administradores)   
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> CreateClient(Guid id, [FromBody] CreateClientCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(GetClientById), new { id = result.Value }, result.Value) : BadRequest(result.Errors);
        }

    }
}
