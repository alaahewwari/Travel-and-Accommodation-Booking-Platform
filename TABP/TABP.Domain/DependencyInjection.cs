using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Services.Booking;
using TABP.Domain.Services.Pricing;
namespace TABP.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPricingService, PricingService>();
            return services;
        }
    }
}