using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            // Si se borra un Client, eliminar todas sus citas
            builder.HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // Borrar citas del cliente

            // Si se borra un Agent, NO eliminar sus citas (deben reasignarse)
            builder.HasOne(a => a.Agent)
                .WithMany(a => a.Appointments)
                .HasForeignKey(a => a.AgentId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación accidental

            // Si se borra una Property, eliminar todas sus citas
            builder.HasOne(a => a.Property)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PropertyId)
                .OnDelete(DeleteBehavior.Cascade); // Borrar citas de la propiedad
        }
    }
}
