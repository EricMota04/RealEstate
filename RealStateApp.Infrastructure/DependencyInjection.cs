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

            // Repositories
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Mappings
            services.AddAutoMapper(typeof(AgentProfile));
            services.AddAutoMapper(typeof(AppointmentProfile));
            services.AddAutoMapper(typeof(ClientProfile));
            services.AddAutoMapper(typeof(UserProfile));


            return services;
        }
    }
}
