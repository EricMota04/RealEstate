using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Domain.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SenderId { get; set; } // Cliente o Agente

        [Required]
        public Guid ReceiverId { get; set; } // Cliente o Agente

        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public Guid ConversationId { get; set; }

        public virtual Conversation Conversation { get; set; }
    }
}
