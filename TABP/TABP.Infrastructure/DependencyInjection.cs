using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;
using System.Text;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure.Common;
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
            services.AddScoped<SieveProcessor>();
            services.AddScoped<SieveProcessor, HotelSieveProcessor>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.Configure<SmtpSettings>(
            configuration.GetSection("Smtp"));
            services.AddTransient<IEmailService, SmtpEmailService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddTransient<IEmailMessageBuilder, EmailMessageBuilder>();
            services.AddTransient<IPdfService,PdfService>();
            services.AddTransient<IInvoiceTemplateBuilder, InvoiceTemplateBuilder>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtConfig = configuration.GetSection("Jwt").Get<JwtConfigurations>();
                var key = Encoding.UTF8.GetBytes(jwtConfig!.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}