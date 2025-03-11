using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class PropertyFavoriteConfiguration : IEntityTypeConfiguration<PropertyFavorite>
    {
        public void Configure(EntityTypeBuilder<PropertyFavorite> builder)
        {
            builder.HasKey(pf => new {pf.PropertyId, pf.ClientId});

            builder.HasOne(pf => pf.Client)
                .WithMany(c => c.Favorites)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pf => pf.Property)
                .WithMany(p => p.PropertyFavorites)
                .HasForeignKey(pf => pf.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
