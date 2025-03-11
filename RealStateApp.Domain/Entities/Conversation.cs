using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Domain.Entities
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        public Guid AgentId { get; set; }
        public virtual Agent Agent { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
