using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Domain.Entities
{
    public class PropertyImprovement
    {

        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public Guid ImprovementId { get; set; }

        // Relaciones
        public virtual Property Property { get; set; }
        public virtual Improvement Improvement { get; set; }
    }
}
