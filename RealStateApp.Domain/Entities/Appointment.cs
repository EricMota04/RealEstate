using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }  // Se usará el mismo `Id` del `Request`

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
        public DateTime AppointmentDate { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } // Scheduled, Completed, Canceled

        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación con `Request`
        public virtual Request Request { get; set; }
    }
}
