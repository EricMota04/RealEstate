using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.DTOs;
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
    public class GetConversationsByClientHandler : IRequestHandler<GetConversationsByClientQuery, ServiceResult<PagedResult<ConversationDto>>>
    {
        private readonly IConversationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetConversationsByClientHandler> _logger;

        public GetConversationsByClientHandler(IConversationRepository repository, IMapper mapper, ILogger<GetConversationsByClientHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<PagedResult<ConversationDto>>> Handle(GetConversationsByClientQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetConversationsByClientAsync(request.ClientId, request.Page, request.PageSize);


                return ServiceResult<PagedResult<ConversationDto>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get client conversations.");
                return ServiceResult<PagedResult<ConversationDto>>.Failure("An error occurred while fetching client conversations.");
            }
        }
    }

}
