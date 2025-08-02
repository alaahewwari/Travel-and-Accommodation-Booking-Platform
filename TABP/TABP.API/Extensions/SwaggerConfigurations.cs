using Microsoft.OpenApi.Models;
using System.Reflection;
using TABP.API.Helpers;
namespace TABP.API.Configurations
{
    /// <summary>
    /// Extension methods for configuring Swagger/OpenAPI documentation generation.
    /// Provides comprehensive API documentation with XML comments, authentication, and custom schema filters.
    /// </summary>
    public static class SwaggerConfigurations
    {
        /// <summary>
        /// Configures Swagger documentation generation with XML comments, JWT authentication, and custom schema filters.
        /// Sets up OpenAPI specification with security definitions and enum schema filtering for better documentation.
        /// </summary>
        /// <param name="services">The service collection to register Swagger services with.</param>
        /// <returns>The service collection for method chaining.</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "TABP.API.xml");
                options.IncludeXmlComments(xmlPath);
                options.SchemaFilter<EnumSchemaFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "TABP API",
                    Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {token}'"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            });
        }
    }
}