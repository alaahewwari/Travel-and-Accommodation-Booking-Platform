using MediatR;
using TABP.API.Behaviors;
namespace TABP.API.Extensions
{
    /// <summary>
    /// Extension methods for configuring MediatR pipeline behaviors.
    /// Provides registration of cross-cutting concerns like logging, validation, and performance monitoring.
    /// </summary>
    public static class PipelineBehaviorExtensions
    {
        /// <summary>
        /// Registers MediatR pipeline behaviors for request processing.
        /// Adds logging behavior to capture request execution information and performance metrics.
        /// </summary>
        /// <param name="services">The service collection to register pipeline behaviors with.</param>
        public static void AddPipelineBehaviors(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));
        }
    }
}