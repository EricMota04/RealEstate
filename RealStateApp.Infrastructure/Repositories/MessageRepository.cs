using Microsoft.EntityFrameworkCore;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly RealEstateDbContext _context;

        public MessageRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationAsync(Guid conversationId)
        {
            return await _context.Messages
                            .Where(m => m.ConversationId == conversationId)
                            .OrderBy(m => m.SentAt)
                            .ToListAsync();
        }
    }

}
