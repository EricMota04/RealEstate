using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RealEstateApp.Application.Features.Chat.Commands;
using RealEstateApp.Domain.Entities;
using System.Text.RegularExpressions;

namespace RealEstate.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(Guid clientId, Guid agentId, Guid propertyId, Guid senderId, Guid receiverId, string content)
        {
            var result = await _mediator.Send(new SendMessageCommand(
                clientId, agentId, propertyId, senderId, receiverId, content
            ));

            if (!result.IsSuccess)
            {
                await Clients.Caller.SendAsync("ErrorSendingMessage", result.Errors);
                return;
            }

            // Agrupar por conversación (usamos ConversationId en frontend también)
            await Clients.Group(result.Value.ConversationId.ToString())
                .SendAsync("ReceiveMessage", result.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var conversationId = Context.GetHttpContext()?.Request?.Query["conversationId"];

            if (!string.IsNullOrWhiteSpace(conversationId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
            }

            await base.OnConnectedAsync();
        }
    }

}
