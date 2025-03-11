using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(user => user.ClientProfile)
                .WithOne(client => client.User)
                .HasForeignKey<Client>(client => client.UserId);

            builder.HasOne(user => user.AgentProfile)
                .WithOne(agent => agent.User)
                .HasForeignKey<Agent>(agent => agent.UserId);
        }
    }
}
