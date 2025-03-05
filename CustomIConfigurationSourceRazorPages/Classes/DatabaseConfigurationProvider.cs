using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CustomIConfigurationSourceRazorPages.Classes;

/// <summary>
/// Provides a custom configuration provider that integrates settings from multiple sources,
/// including JSON configuration files and a database, with support for caching.
/// </summary>
public class DatabaseConfigurationProvider : ConfigurationProvider
{
    private readonly IConfiguration _jsonConfiguration;
    private readonly string? _connectionString;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration;

    public DatabaseConfigurationProvider(IConfiguration jsonConfiguration, string? connectionString, IMemoryCache cache, TimeSpan cacheDuration)
    {
        _jsonConfiguration = jsonConfiguration;
        _connectionString = connectionString;
        _cache = cache;
        _cacheDuration = cacheDuration;
    }

    public override void Load()
    {
        var settings = new Dictionary<string, string>();

        // Load settings from appsettings.json first
        foreach (var kvp in _jsonConfiguration.AsEnumerable())
        {
            settings[kvp.Key] = kvp.Value;
        }

        // Load settings from the database with caching
        var options = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(_connectionString)
            .Options;

        using (var context = new Context(options))
        {
            foreach (var setting in context.ConfigurationSettings)
            {
                settings[setting.Key] = setting.Value;  // Database values override JSON values

                // Cache the value
                _cache.Set(setting.Key, setting.Value, _cacheDuration);
            }
        }

        Data = settings; // Ensure data is set for IConfiguration
    }
}