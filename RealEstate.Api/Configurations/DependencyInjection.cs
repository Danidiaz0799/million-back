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
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IMapperService, MapperService>();
            return services;
        }
    }
}
