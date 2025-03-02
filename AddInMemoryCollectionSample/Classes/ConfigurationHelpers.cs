using Microsoft.Extensions.Configuration;

namespace AddInMemoryCollectionSample.Classes
{
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
        /// Builds and returns an <see cref="IConfigurationRoot"/> instance by loading configuration
        /// settings from the current directory and a JSON file named "appsettings.json".
        /// </summary>
        /// <remarks>
        /// The method sets the base path to the current directory and ensures that the 
        /// "appsettings.json" file is mandatory. It also enables automatic reloading of 
        /// configuration settings when the file changes.
        /// </remarks>
        /// <returns>
        /// An <see cref="IConfigurationRoot"/> instance containing the loaded configuration settings.
        /// </returns>
        public static IConfigurationRoot configurationBuilder() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
    }
}
