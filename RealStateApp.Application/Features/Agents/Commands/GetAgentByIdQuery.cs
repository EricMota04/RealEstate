using MediatR;
using RealEstateApp.Application.DTOs.Agent;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Commands
{
    public class GetAgentByIdQuery : IRequest<ServiceResult<AgentDto>>
    {
        public Guid AgentId { get; set; }

        public GetAgentByIdQuery(Guid agentId)
        {
            AgentId = agentId;
        }
    }
}
