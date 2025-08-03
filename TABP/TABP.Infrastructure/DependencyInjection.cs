using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;
using System.Text;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure.Configurations;
using TABP.Infrastructure.Services;
using TABP.Infrastructure.Services.Email;
using TABP.Infrastructure.Sieve;
namespace TABP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddSieveServices(services);
            AddEmailServices(services, configuration);
            AddJwtAuthentication(services, configuration);
            AddPdfServices(services, configuration);
            AddCloudinaryServices(services, configuration);
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddTransient<IInvoiceTemplateBuilder, InvoiceTemplateBuilder>();
            return services;
        }
        private static void AddSieveServices(IServiceCollection services)
        {
            services.AddScoped<SieveProcessor>();
            services.AddScoped<SieveProcessor, HotelSieveProcessor>();
        }
        private static void AddEmailServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection(SmtpSettings.SectionName));
            services.AddTransient<IEmailService, SmtpEmailService>();
            services.AddTransient<IEmailMessageBuilder, EmailMessageBuilder>();
        }
        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfigurations>();
            var key = Encoding.UTF8.GetBytes(jwtConfig!.Key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            services.AddAuthorization();
        }
        private static void AddPdfServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IronPdfConfiguration>(configuration.GetSection(IronPdfConfiguration.SectionName));
            services.AddScoped<IPdfService, PdfService>();
        }
        private static void AddCloudinaryServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(configuration.GetSection(CloudinarySettings.SectionName));
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddSingleton(provider =>
            {
                var settings = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
                return new Cloudinary(account);
            });
        }
    }
}
