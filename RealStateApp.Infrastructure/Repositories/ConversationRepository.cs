using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly RealEstateDbContext _context;
        private readonly IMapper _mapper;

        public ConversationRepository(RealEstateDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Conversation?> GetByParticipantsAndPropertyAsync(Guid clientId, Guid agentId, Guid propertyId)
        {
            return await _context.Conversations
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c =>
                    c.ClientId == clientId &&
                    c.AgentId == agentId &&
                    c.PropertyId == propertyId);
        }

        public async Task<Conversation> CreateAsync(Conversation conversation)
        {
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }

        public async Task<PagedResult<ConversationDto>> GetConversationsByAgentAsync(Guid agentId, int page = 1, int pageSize = 10)
        {
            var query = _context.Conversations
                .Include(c => c.Client)
                .Include(c => c.Property)
                .Where(c => c.AgentId == agentId)
                .OrderByDescending(c => c.CreatedAt);

            var total = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = data.Select(c => new ConversationDto
            {
                Id = c.Id,
                ClientId = c.ClientId,
                AgentId = c.AgentId,
                PropertyId = c.PropertyId,
                CreatedAt = c.CreatedAt,
                Title = $"{c.Client.User.FirstName} {c.Client.User.LastName}"
            }).ToList();

            return new PagedResult<ConversationDto>(dtos, total, page, pageSize);
        }


        public async Task<PagedResult<ConversationDto>> GetConversationsByClientAsync(Guid clientId, int page = 1, int pageSize = 10)
        {
            var query = _context.Conversations
                .Include(c => c.Agent)
                .Include(c => c.Property)
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.CreatedAt);

            var total = await query.CountAsync();
            var data = await query.Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            var dtos = data.Select(c => new ConversationDto
            {
                Id = c.Id,
                ClientId = c.ClientId,
                AgentId = c.AgentId,
                PropertyId = c.PropertyId,
                CreatedAt = c.CreatedAt,
                Title = $"{c.Agent.User.FirstName} {c.Agent.User.LastName}"
            }).ToList();


            return new PagedResult<ConversationDto>(dtos, total, page, pageSize);
        }
    }

}
