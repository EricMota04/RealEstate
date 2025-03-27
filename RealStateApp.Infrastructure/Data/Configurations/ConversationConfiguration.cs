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
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Client)
                .WithMany(c => c.Conversations)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Agent)
                .WithMany(c => c.Conversations)
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Property)
                .WithMany(p => p.Conversations)
                .HasForeignKey(c => c.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
