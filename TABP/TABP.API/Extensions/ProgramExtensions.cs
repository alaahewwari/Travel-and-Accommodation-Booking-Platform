using MediatR;
using Serilog;
using Serilog.Events;
using TABP.API.Behaviors;
using TABP.API.Configurations;
using TABP.API.Middlewares;
using TABP.API.Services;
using TABP.Application;
using TABP.Domain;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure;
using TABP.Infrastructure.Configurations;
using TABP.Persistence;
namespace TABP.API.Extensions
{
    public static class ProgramExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var configuration = BuildConfiguration();
            builder.Services.Configure<JwtConfigurations>(configuration.GetSection("Jwt"));
            builder.AddLayerServices(configuration);
            builder.AddApiServices();
            builder.AddPipelineBehaviors();
        }
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));
        }
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
        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        private static void AddLayerServices(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(configuration);
            builder.Services.AddPersistenceServices(configuration);
            builder.Services.AddDomainServices();
        }
        private static void AddApiServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwagger();
            builder.Services.AddControllers();
            builder.Services.AddApiValidation();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOutputCache();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            builder.Services.AddScoped<IUserContext, UserContext>();
        }
        private static void AddPipelineBehaviors(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestLoggingPipelineBehavior<,>));
        }
        private static void ConfigureRequestLogging(this WebApplication app)
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
        private static void ConfigureDevelopmentMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}