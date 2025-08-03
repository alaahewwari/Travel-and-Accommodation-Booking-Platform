using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
namespace TABP.API.Extensions
{
    /// <summary>
    /// Extension methods for configuring FluentValidation services in the API.
    /// Provides automatic validation registration and integration with ASP.NET Core MVC.
    /// </summary>
    public static class ValidatorConfigurations
    {
        /// <summary>
        /// Registers all FluentValidation validators from the executing assembly and enables automatic validation.
        /// Scans the current assembly for validator classes and configures automatic validation for controller actions.
        /// </summary>
        /// <param name="services">The service collection to register validation services with.</param>
        /// <returns>The service collection for method chaining.</returns>
        public static IServiceCollection AddApiValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}