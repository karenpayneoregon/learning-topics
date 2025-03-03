using ConsoleConfigurationLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace CustomIConfigurationSourceSample.Classes;

/// <summary>
/// Represents a custom implementation of <see cref="Microsoft.Extensions.Configuration.IConfigurationSource"/> 
/// that integrates configuration settings from a JSON file and a database.
/// </summary>
/// <remarks>
/// This class is responsible for creating an instance of <see cref="DatabaseConfigurationProvider"/>, 
/// which merges configuration settings from an appsettings.json file and a database. 
/// It ensures that database settings take precedence over JSON settings in case of conflicts.
/// </remarks>
public class DatabaseConfigurationSource : IConfigurationSource
{
    private readonly IConfigurationRoot _jsonConfiguration;
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseConfigurationSource"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor loads configuration settings from the "appsettings.json" file and retrieves
    /// the database connection string specified under the "DefaultConnection" key. These settings
    /// are used to integrate JSON and database configurations.
    /// </remarks>
    public DatabaseConfigurationSource()
    {
        // Load appsettings.json inside DatabaseConfigurationSource
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _jsonConfiguration = builder.Build();
        _connectionString = _jsonConfiguration.GetConnectionString(nameof(ConnectionStrings.MainConnection));
    }

    /// <summary>
    /// Builds the <see cref="IConfigurationProvider"/> for this source.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IConfigurationBuilder"/> used to construct the provider.
    /// </param>
    /// <returns>
    /// An instance of <see cref="DatabaseConfigurationProvider"/> that merges 
    /// configuration settings from a JSON file and a database.
    /// </returns>
    /// <remarks>
    /// This method creates a <see cref="DatabaseConfigurationProvider"/> using 
    /// the JSON configuration and database connection string initialized in the 
    /// <see cref="DatabaseConfigurationSource"/>. The resulting provider ensures 
    /// that database settings override JSON settings in case of conflicts.
    /// </remarks>
    public IConfigurationProvider Build(IConfigurationBuilder builder) 
        => new DatabaseConfigurationProvider(_jsonConfiguration, _connectionString);
}