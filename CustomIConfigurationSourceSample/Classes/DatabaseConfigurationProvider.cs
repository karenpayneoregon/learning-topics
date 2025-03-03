using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CustomIConfigurationSourceSample.Classes;

/// <summary>
/// Provides a custom implementation of <see cref="ConfigurationProvider"/> 
/// that loads configuration settings from both a JSON file and a database.
/// </summary>
/// <remarks>
/// This class combines configuration settings from an appsettings.json file and a database. 
/// Database settings take precedence over JSON settings when there are conflicts.
/// </remarks>
public class DatabaseConfigurationProvider : ConfigurationProvider
{
    private readonly IConfigurationRoot _jsonConfiguration;
    private readonly string _connectionString;

    public DatabaseConfigurationProvider(IConfigurationRoot jsonConfiguration, string connectionString)
    {
        _jsonConfiguration = jsonConfiguration;
        _connectionString = connectionString;
    }

    /// <summary>
    /// Loads configuration settings from both a JSON file and a database, 
    /// merging them into a single configuration source.
    /// </summary>
    /// <remarks>
    /// This method first loads configuration settings from the provided JSON configuration. 
    /// It then retrieves additional settings from the database, overriding any conflicting keys 
    /// with the values from the database.
    /// </remarks>
    public override void Load()
    {
        var settings = new Dictionary<string, string>();

        // Load settings from appsettings.json first
        foreach (var kvp in _jsonConfiguration.AsEnumerable())
        {
            settings[kvp.Key] = kvp.Value;
        }

        // Load settings from the database
        var options = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new Context(options))
        {
            foreach (var setting in context.ConfigurationSettings)
            {
                settings[setting.Key] = setting.Value;  // Database values override JSON values
            }
        }

        Data = settings;
    }

}