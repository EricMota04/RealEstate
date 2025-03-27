using MediatR;
using RealEstateApp.Application.DTOs.Chat;
using RealEstateApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Chat.Commands
{
    public record SendMessageCommand(
    Guid ClientId,
    Guid AgentId,
    Guid PropertyId,
    Guid SenderId,
    Guid ReceiverId,
    string Content
) : IRequest<ServiceResult<MessageDto>>;

}
