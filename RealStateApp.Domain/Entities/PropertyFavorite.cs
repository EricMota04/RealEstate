using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Domain.Entities
{
    public class PropertyFavorite
    {

        [Required]
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }

        [Required]
        public Guid PropertyId { get; set; }
        public virtual Property Property { get; set; }

        [Required]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
