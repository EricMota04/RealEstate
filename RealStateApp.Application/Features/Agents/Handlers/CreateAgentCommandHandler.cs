using MediatR;
using RealEstate.Shared.Enums.Agent;
using RealEstateApp.Application.Features.Agents.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Validators.Agent;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Handlers
{
    public class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, ServiceResult<Guid>>
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IUserRepository _userRepository;

        public CreateAgentCommandHandler(IAgentRepository agentRepository, IUserRepository userRepository)
        {
            _agentRepository = agentRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<Guid>> Handle(CreateAgentCommand request, CancellationToken cancellationToken)
        {
            var agentValidation = new CreateAgentCommandValidator().Validate(request);

            if (!agentValidation.IsValid)
                return ServiceResult<Guid>.Failure(agentValidation.Errors.Select(e => e.ErrorMessage).ToList());

            // Verificar si el usuario existe antes de convertirlo en Agente
            var user = await _userRepository.FindByIdAsync(request.UserId);
            if (user == null)
                return ServiceResult<Guid>.Failure("User not found.");

            // Verificar si el usuario ya es un Agente
            var agentExists = await _agentRepository.ExistsAsync(a => a.UserId == request.UserId);
            if (agentExists)
                return ServiceResult<Guid>.Failure("User is already an agent.");

            // Crear el perfil de Agente
            var agent = new Agent
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Status = AgentStatus.Active
            };

            await _agentRepository.AddAsync(agent);

            return ServiceResult<Guid>.Success(agent.Id);
        }
    }

}
