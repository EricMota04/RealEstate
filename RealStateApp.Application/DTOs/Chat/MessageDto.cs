using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Application.DTOs.Chat
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ConversationId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }

}
