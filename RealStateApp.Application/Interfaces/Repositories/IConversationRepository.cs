using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Application.Interfaces.Repositories
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByParticipantsAndPropertyAsync(Guid clientId, Guid agentId, Guid propertyId);
        Task<Conversation> CreateAsync(Conversation conversation);
        Task<PagedResult<ConversationDto>> GetConversationsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10);
        Task<PagedResult<ConversationDto>> GetConversationsByClientAsync(Guid clientId, int page = 1, int pageSize = 10);

    }
}
