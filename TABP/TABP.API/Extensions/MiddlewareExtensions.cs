using TABP.API.Middlewares;
namespace TABP.API.Extensions
{
    /// <summary>
    /// Extension methods for configuring HTTP request pipeline middleware.
    /// Provides centralized configuration of exception handling, authentication, and development tools.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Configures the complete HTTP request pipeline with exception handling, authentication, and routing.
        /// Sets up middleware in the correct order for security, logging, and request processing.
        /// </summary>
        /// <param name="app">The web application to configure middleware for.</param>
        public static void ConfigureMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.ConfigureRequestLogging();
            app.ConfigureDevelopmentMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
        /// <summary>
        /// Configures development-specific middleware including Swagger documentation.
        /// Only enables Swagger UI and documentation generation in development environment.
        /// </summary>
        /// <param name="app">The web application to configure development middleware for.</param>
        public static void ConfigureDevelopmentMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}