using RealEstate.Application.Interfaces;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Configurations;
using RealEstate.Infrastructure.Repositories;

namespace RealEstate.Api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB Configuration
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            
            // Repositories
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
            
            // Services
            services.AddScoped<IMapperService, MapperService>();
            
            return services;
        }
    }
}
