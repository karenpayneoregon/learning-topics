using Microsoft.Extensions.Configuration;

namespace StringsBetweenQuotesExample.Classes;

/// <summary>
/// Provides helper methods for configuration management, including building 
/// and retrieving configuration settings from various sources.
/// </summary>
/// <remarks>
/// Consider moving this class to a separate class project for easy reuse.
/// </remarks>
public class ConfigurationHelpers
{
    /// <summary>
    /// Builds an <see cref="IConfigurationRoot"/> instance by loading configuration settings 
    /// from JSON files in the current directory.
    /// </summary>
    /// <remarks>
    /// This method reads the "ASPNETCORE_ENVIRONMENT" environment variable to determine the 
    /// current environment, defaulting to "Development" if not specified. It loads the 
    /// "appsettings.json" file as mandatory and an optional environment-specific configuration 
    /// file (e.g., "appsettings.Development.json"). Configuration settings are automatically 
    /// reloaded when the files change.
    /// </remarks>
    /// <returns>
    /// An <see cref="IConfigurationRoot"/> instance containing the loaded configuration settings.
    /// </returns>
    public static IConfigurationRoot ConfigurationBuilder() =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", 
                optional: true, reloadOnChange: true)
            .Build();
}