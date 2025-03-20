using AutoMapper;
using MediatR;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.Features.Agents.Queries;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Handlers
{
    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, ServiceResult<AgentDto>>
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;

        public GetAgentByIdQueryHandler(IAgentRepository agentRepository, IMapper mapper)
        {
            _agentRepository = agentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<AgentDto>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await _agentRepository.FindByIdAsync(request.AgentId);
            if (agent == null)
                return ServiceResult<AgentDto>.Failure("Agent not found.");

            var agentDto = _mapper.Map<AgentDto>(agent);
            return ServiceResult<AgentDto>.Success(agentDto);
        }
    }
}
