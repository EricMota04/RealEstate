using MediatR;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.DTOs;
using RealEstateApp.Application.Wrappers;

namespace RealEstateApp.Application.Features.Chat.Querys
{
    public record GetConversationsByAgentQuery(Guid AgentId, int Page = 1, int PageSize = 10)
    : IRequest<ServiceResult<PagedResult<ConversationDto>>>;

}
