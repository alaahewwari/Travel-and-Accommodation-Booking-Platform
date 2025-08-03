using TABP.Infrastructure;
using TABP.Persistence;
using TABP.Application;
using TABP.Domain;
namespace TABP.API.Extensions
{
    /// <summary>
    /// Extension methods for registering services from all application layers.
    /// Provides centralized registration of infrastructure, persistence, application, and domain layer services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers services from all application layers including infrastructure, persistence, application, and domain.
        /// Configures the complete service stack required for the Travel and Booking Platform API.
        /// </summary>
        /// <param name="services">The service collection to register layer services with.</param>
        /// <param name="configuration">The application configuration containing connection strings and settings.</param>
        public static void AddLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddPersistenceServices(configuration);
            services.AddApplicationServices();
            services.AddDomainServices();
        }
    }
}