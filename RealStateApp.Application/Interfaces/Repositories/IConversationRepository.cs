using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IConversationRepository : IBaseRepository<Conversation>
    {
        Task<PagedResult<Conversation>> GetConversationsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
        Task<PagedResult<Conversation>> GetConversationsByClientAsync(Guid clientId, int page = 1, int pageSize = 10);

    }
}
