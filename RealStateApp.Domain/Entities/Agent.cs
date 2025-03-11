using RealEstate.Shared.Enums.Agent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Domain.Entities
{
    public class Agent
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]      
        public AgentStatus Status { get; set; }

        // Propiedades gestionadas por el agente
        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

        // Requests de citas recibidas por el agente
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

        // Citas confirmadas del agente
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // Mensajes enviados/recibidos con clientes
        public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
    }
}
