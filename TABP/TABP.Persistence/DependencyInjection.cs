using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace TABP.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException("This method should be implemented to register persistence services.");
        }
    }
}