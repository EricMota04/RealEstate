using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.Shared.Enums.Property;

namespace RealEstateApp.Domain.Entities
{
    public class Property
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(6)]
        public string Code { get; set; } // Se generará automáticamente

        [Required]
        public Guid PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; } // Relación con Tipo de Propiedad

        [Required]
        public Guid SaleTypeId { get; set; }
        public virtual SaleType SaleType { get; set; } // Relación con Tipo de Venta

        [Required]
        [Range(100, int.MaxValue, ErrorMessage = "The price should be at least 100")]
        public int Price { get; set; }

        [Required]
        [Range(10, int.MaxValue, ErrorMessage = "The size should be at least 10")]
        public double SizeInMeters { get; set; }

        [Required]
        public int NumberOfRooms { get; set; }

        [Required]
        public int NumberOfBathrooms { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime DatePublished { get; set; } = DateTime.UtcNow;

        [Required]
        public PropertyStatus Status { get; set; } // Disponible o Vendida

        [Required]
        public Guid AgentId { get; set; }
        public virtual Agent Agent { get; set; } // Relación con Agente

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Request> Requests { get; set; } = new List<Request>(); // Relación con Solicitudes
        public virtual ICollection<PropertyFavorite> PropertyFavorites { get; set; }

        // Relación Muchos a Muchos con Mejoras
        public virtual ICollection<PropertyImprovement> PropertyImprovements { get; set; } = new List<PropertyImprovement>();

        // Relación con Imágenes
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
