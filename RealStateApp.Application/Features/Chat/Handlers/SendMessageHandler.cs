using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.Features.Chat.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Chat.Handlers
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, ServiceResult<MessageDto>>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SendMessageHandler> _logger;

        public SendMessageHandler(
            IConversationRepository conversationRepository,
            IMessageRepository messageRepository,
            IMapper mapper,
            ILogger<SendMessageHandler> logger)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<MessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Check if conversation exists
                var conversation = await _conversationRepository
                    .GetByParticipantsAndPropertyAsync(request.ClientId, request.AgentId, request.PropertyId);

                // 2. If not, create it
                if (conversation == null)
                {
                    conversation = new Conversation
                    {
                        Id = Guid.NewGuid(),
                        ClientId = request.ClientId,
                        AgentId = request.AgentId,
                        PropertyId = request.PropertyId,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _conversationRepository.CreateAsync(conversation);
                }

                // 3. Create the message
                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    ConversationId = conversation.Id,
                    SenderId = request.SenderId,
                    ReceiverId = request.ReceiverId,
                    Content = request.Content,
                    SentAt = DateTime.UtcNow
                };

                await _messageRepository.AddAsync(message);

                var dto = _mapper.Map<MessageDto>(message);
                return ServiceResult<MessageDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the message.");
                return ServiceResult<MessageDto>.Failure("An error occurred while sending the message.");
            }
        }
    }



}
