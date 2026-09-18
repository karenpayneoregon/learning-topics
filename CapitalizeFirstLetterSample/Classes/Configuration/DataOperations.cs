using Microsoft.Extensions.Configuration;

namespace CapitalizeFirstLetterSample.Classes.Configuration
{
    /// <summary>
    /// Performs data operations.
    /// </summary>
    internal class DataOperations
    {
        public static IConfigurationBuilder ConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}
