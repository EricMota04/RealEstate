using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstateApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RealEstateApp.Application.AssemblyReference).Assembly));
            services.AddValidatorsFromAssembly(typeof(RealEstateApp.Application.AssemblyReference).Assembly);
            return services;
        }
    }
}
