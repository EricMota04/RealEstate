using MediatR;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Chat.Querys
{
    public record GetMessagesByConversationIdQuery(Guid ConversationId) : IRequest<ServiceResult<List<MessageDto>>>;
}
