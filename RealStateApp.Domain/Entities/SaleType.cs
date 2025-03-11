using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Domain.Entities
{
    public class SaleType
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Relación con propiedades (1 Tipo de Venta puede tener muchas Propiedades)
        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
