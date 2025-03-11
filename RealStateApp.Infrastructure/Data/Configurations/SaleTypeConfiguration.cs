using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class SaleTypeConfiguration : IEntityTypeConfiguration<SaleType>
    {
        public void Configure(EntityTypeBuilder<SaleType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Properties)
                .WithOne(e => e.SaleType)
                .HasForeignKey(e => e.SaleTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
