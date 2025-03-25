using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.DTOs.Chat
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid AgentId { get; set; }
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = string.Empty; // Nombre del otro usuario
        public DateTime CreatedAt { get; set; }
    }

}
