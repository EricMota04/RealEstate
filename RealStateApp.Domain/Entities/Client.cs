using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Domain.Entities
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        // Propiedades favoritas del cliente
        public virtual ICollection<PropertyFavorite> Favorites { get; set; } = new List<PropertyFavorite>();

        // Requests de citas enviadas por el cliente
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
        public virtual ICollection<Conversation> Conversations { get; set; }

        // Citas confirmadas del cliente
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
