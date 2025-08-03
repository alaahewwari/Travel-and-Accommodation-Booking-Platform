using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace TABP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException("This method should be implemented to register infrastructure services.");
        }
    }
}