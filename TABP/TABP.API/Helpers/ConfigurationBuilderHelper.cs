namespace TABP.API.Extensions
{
    /// <summary>
    /// Helper class for building application configuration from JSON files.
    /// Provides centralized configuration setup for the Travel and Booking Platform API.
    /// </summary>
    public static class ConfigurationBuilderHelper
    {
        /// <summary>
        /// Builds the application configuration from appsettings.json file.
        /// Sets the base path to the current directory and loads configuration from the standard appsettings file.
        /// </summary>
        /// <returns>The built configuration object containing all application settings.</returns>
        public static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}