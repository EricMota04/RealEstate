using Microsoft.EntityFrameworkCore;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data.Configurations;

namespace RealEstateApp.Infrastructure.Data
{
    public class RealEstateDbContext : DbContext
    {
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyFavorite> PropertyFavorites { get; set; }
        public DbSet<PropertyImprovement> PropertyImprovements { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<SaleType> SaleTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public RealEstateDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AgentConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new ImprovementsConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new PropertyFavoriteConfiguration());
            builder.ApplyConfiguration(new PropertyImprovementsConfiguration());
            builder.ApplyConfiguration(new PropertyTypeConfiguration());
            builder.ApplyConfiguration(new RequestConfiguration());
            builder.ApplyConfiguration(new SaleTypeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
