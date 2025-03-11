using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class PropertyImprovementsConfiguration : IEntityTypeConfiguration<PropertyImprovement>
    {
        public void Configure(EntityTypeBuilder<PropertyImprovement> builder)
        {
            // Definir clave compuesta
            builder.HasKey(pi => new { pi.PropertyId, pi.ImprovementId });

            builder.HasOne(pi => pi.Improvement)
                .WithMany(i => i.PropertyImprovements)
                .HasForeignKey(pi => pi.ImprovementId)
                .OnDelete(DeleteBehavior.Cascade); // Borrar mejoras si la propiedad se elimina

            builder.HasOne(pi => pi.Property)
                .WithMany(p => p.PropertyImprovements)
                .HasForeignKey(pi => pi.PropertyId)
                .OnDelete(DeleteBehavior.Cascade); // Borrar la relación si la propiedad se elimina
        }
    }
}
