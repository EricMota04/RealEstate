using MediatR;
using RealEstateApp.Application.Features.Agents.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Handlers
{
    public class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, ServiceResult<bool>>
    {
        private readonly IAgentRepository _agentRepository;

        public UpdateAgentCommandHandler(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<ServiceResult<bool>> Handle(UpdateAgentCommand request, CancellationToken cancellationToken)
        {
            var success = await _agentRepository.ChangeAgentStatus(request.AgentId, request.Status);

            return success
                ? ServiceResult<bool>.Success(true)
                : ServiceResult<bool>.Failure("Agent not found or status update failed.");
        }
    }
}
