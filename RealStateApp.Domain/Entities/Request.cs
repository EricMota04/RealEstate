using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Domain.Entities
{
    public class Request
    {
        [Key]
        public Guid Id { get; set; }  // Se usará también en `Appointment` si se aprueba

        [Required]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        public Guid PropertyId { get; set; }
        public virtual Property Property { get; set; }

        [Required]
        public Guid AgentId { get; set; }
        public virtual Agent Agent { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }

        [Required]
        public RequestStatus Status { get; set; } // Pending, Approved, Rejected

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación con `Appointment` cuando el Request es aprobado
        public virtual Appointment? Appointment { get; set; }
    }
}
