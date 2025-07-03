using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TABP.Application.Cities.Mapping;
namespace TABP.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<CityMapper>();
            return services;
        }
    }
}