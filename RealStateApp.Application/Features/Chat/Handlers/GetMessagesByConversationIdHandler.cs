using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.Features.Chat.Querys;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Chat.Handlers
{
    public class GetMessagesByConversationIdHandler : IRequestHandler<GetMessagesByConversationIdQuery, ServiceResult<List<MessageDto>>>
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetMessagesByConversationIdHandler> _logger;

        public GetMessagesByConversationIdHandler(IMessageRepository repository, IMapper mapper, ILogger<GetMessagesByConversationIdHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<List<MessageDto>>> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _repository.GetMessagesByConversationAsync(request.ConversationId);
                var dto = _mapper.Map<List<MessageDto>>(messages);
                return ServiceResult<List<MessageDto>>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get messages.");
                return ServiceResult<List<MessageDto>>.Failure("An error occurred while fetching messages.");
            }
        }
    }

}
