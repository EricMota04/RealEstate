using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Client)
                .WithMany(c => c.Requests)
                .HasForeignKey(e => e.ClientId);

            builder.HasOne(e => e.Property)
                .WithMany(p => p.Requests)
                .HasForeignKey(e => e.PropertyId);

            builder.HasOne(e => e.Agent)
                .WithMany(a => a.Requests)
                .HasForeignKey(e => e.AgentId);

            builder.HasOne(e => e.Appointment)
                .WithOne(a => a.Request)
                .HasForeignKey<Appointment>(a => a.Id);
        }
    }
}
