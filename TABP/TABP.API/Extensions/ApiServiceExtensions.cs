using TABP.API.Configurations;
using TABP.API.Services;
using TABP.Domain.Interfaces.Services;
namespace TABP.API.Extensions
{
    /// <summary>
    /// Extension methods for configuring API-specific services in the dependency injection container.
    /// Provides centralized registration of all services required for the Travel and Booking Platform API.
    /// </summary>
    public static class ApiServiceExtensions
    {
        /// <summary>
        /// Registers all API-related services including documentation, validation, caching, and routing.
        /// Configures controllers, Swagger documentation, output caching, and user context services.
        /// </summary>
        /// <param name="services">The service collection to register services with.</param>
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddSwagger();
            services.AddControllers();
            services.AddApiValidation();
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddOutputCache();
            services.AddHttpContextAccessor();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}