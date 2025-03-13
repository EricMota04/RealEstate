using MediatR;
using RealEstate.Shared.Enums.Agent;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Agents.Commands
{
    public class UpdateAgentCommand : IRequest<ServiceResult<bool>>
    {
        public Guid AgentId { get; set; }
        public AgentStatus Status { get; set; }

        public UpdateAgentCommand(Guid agentId, AgentStatus status)
        {
            AgentId = agentId;
            Status = status;
        }
    }
}
