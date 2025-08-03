using Serilog;
using Serilog.Events;
namespace TABP.API.Extensions
{
    /// <summary>
    /// Extension methods for configuring Serilog logging throughout the application.
    /// Provides structured logging configuration and HTTP request logging with appropriate log levels.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Configures Serilog as the primary logging provider using application configuration.
        /// Sets up structured logging with configuration-driven sinks, formatters, and log levels.
        /// </summary>
        /// <param name="builder">The web application builder to configure logging for.</param>
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));
        }
        /// <summary>
        /// Configures HTTP request logging with intelligent log level determination.
        /// Logs requests at appropriate levels based on response status codes and exception presence.
        /// </summary>
        /// <param name="app">The web application to configure request logging for.</param>
        public static void ConfigureRequestLogging(this WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (httpContext, elapsed, ex) =>
                {
                    if (ex != null || httpContext.Response.StatusCode >= 500)
                    {
                        return LogEventLevel.Debug;
                    }
                    return LogEventLevel.Information;
                };
            });
        }
    }
}