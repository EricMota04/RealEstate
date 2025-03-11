using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Domain.Entities
{
    public class Improvement
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Relación con propiedades (Propiedad puede tener muchas mejoras)
        public virtual ICollection<PropertyImprovement> PropertyImprovements { get; set; } = new List<PropertyImprovement>();
    }
}
