using CustomIConfigurationSourceSample.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace CustomIConfigurationSourceSample.Classes;

/// <summary>
/// Provides a custom configuration provider that retrieves configuration settings
/// from both a JSON configuration source and a database.
/// </summary>
/// <remarks>
/// This class extends the <see cref="ConfigurationProvider"/> to support loading
/// configuration data from multiple sources, including a JSON file and a database.
/// Database values take precedence over JSON values, and caching is utilized to
/// optimize performance and reduce database queries.
/// </remarks>
public class DatabaseConfigurationProvider : ConfigurationProvider
{
    private readonly IConfigurationRoot _jsonConfiguration;
    private readonly string _connectionString;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseConfigurationProvider"/> class.
    /// </summary>
    /// <param name="jsonConfiguration">
    /// The root JSON configuration source from which initial configuration settings are loaded.
    /// </param>
    /// <param name="connectionString">
    /// The connection string used to connect to the database for retrieving configuration settings.
    /// </param>
    /// <param name="cache">
    /// The memory cache instance used to store configuration settings retrieved from the database.
    /// </param>
    /// <param name="cacheDuration">
    /// The duration for which database configuration settings should be cached to optimize performance.
    /// </param>
    /// <remarks>
    /// This constructor sets up the <see cref="DatabaseConfigurationProvider"/> to load configuration
    /// settings from both a JSON file and a database. Database settings are cached for the specified
    /// duration to reduce database queries and improve performance.
    /// </remarks>
    public DatabaseConfigurationProvider(IConfigurationRoot jsonConfiguration, string connectionString,
        IMemoryCache cache, TimeSpan cacheDuration)
    {
        _jsonConfiguration = jsonConfiguration;
        _connectionString = connectionString;
        _cache = cache;
        _cacheDuration = cacheDuration;
    }

    public override void Load()
    {
        var settings = new Dictionary<string, string>();

        foreach (var kvp in _jsonConfiguration.AsEnumerable())
            settings[kvp.Key] = kvp.Value;

        var options = DbContextOptions.DbContextOptionsBuilderDevelopment(_connectionString);

        using var context = new Context(options.Options);

        foreach (var setting in context.ConfigurationSettings)
        {
            settings[setting.Key] = setting.Value;
            _cache.Set(setting.Key, setting.Value, _cacheDuration);
        }

        Data = settings;
    }
}
