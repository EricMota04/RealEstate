using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Mappings;
using RealEstateApp.Infrastructure.Data;
using RealEstateApp.Infrastructure.Repositories;
using System.Runtime.CompilerServices;

namespace RealEstateApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RealEstateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RealEstateConnection"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));


            //TODO INJECT THE REST OF THE SERVICES AND REPOS

            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddAutoMapper(typeof(AgentProfile));
            return services;
        }
    }
}
