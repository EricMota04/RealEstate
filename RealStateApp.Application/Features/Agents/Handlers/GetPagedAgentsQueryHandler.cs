using AutoMapper;
using MediatR;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.DTOs;
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
    public class GetPagedAgentsQueryHandler : IRequestHandler<GetPagedAgentsQuery, ServiceResult<PagedResult<AgentDto>>>
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;

        public GetPagedAgentsQueryHandler(IAgentRepository agentRepository, IMapper mapper)
        {
            _agentRepository = agentRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PagedResult<AgentDto>>> Handle(GetPagedAgentsQuery request, CancellationToken cancellationToken)
        {
            var pagedAgents = await _agentRepository.GetAllAsync(request.Page, request.PageSize);


            return ServiceResult<PagedResult<AgentDto>>.Success(pagedAgents);
        }
    }
}
