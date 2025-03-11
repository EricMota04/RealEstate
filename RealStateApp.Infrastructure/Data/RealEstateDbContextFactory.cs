using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RealEstateApp.Infrastructure.Data
{
    public class RealEstateDbContextFactory : IDesignTimeDbContextFactory<RealEstateDbContext>
    {
        public RealEstateDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RealEstateDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("RealEstateConnection"));

            return new RealEstateDbContext(optionsBuilder.Options);
        }
    }
}
