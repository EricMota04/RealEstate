using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Agent)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.Images)
                .WithOne(i => i.Property)
                .HasForeignKey(p => p.PropertyId);


        }
    }
}
