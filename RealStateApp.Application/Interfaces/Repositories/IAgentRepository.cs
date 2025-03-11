using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstate.Shared.Enums.Agent;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IAgentRepository :
        IBaseRepository<Agent>,
        IReadableRepository<Agent>,
        IUpdatableRepository<Agent>
    {
        Task ChangeAgentStatus(Guid agentId, AgentStatus status);
    }
}
