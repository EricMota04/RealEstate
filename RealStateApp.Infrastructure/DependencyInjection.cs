using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Mappings;
using RealEstateApp.Infrastructure.Data;
using RealEstateApp.Infrastructure.Identity;
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

            services.AddSingleton(new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]));
            //TODO INJECT THE REST OF THE SERVICES AND REPOS

            // Repositories
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AgentProfile).Assembly);
            return services;
        }

        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(jwtOptions =>
                {
                    configuration.Bind("AzureAdB2C", jwtOptions);
                    jwtOptions.TokenValidationParameters.NameClaimType = "name";
                    jwtOptions.TokenValidationParameters.RoleClaimType = "extension_UserRole"; // This line is KEY
                }, options =>
                {
                    configuration.Bind("AzureAdB2C", options);
                });

            // Map the custom role claim to standard .NET role
            services.AddTransient<IClaimsTransformation, B2CCustomRoleMapper>();

            return services;
        }


    }
}
