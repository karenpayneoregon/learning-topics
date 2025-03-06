using ConsoleConfigurationLibrary.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CustomIConfigurationSourceRazorPages.Classes;

/// <summary>
/// Represents a custom configuration source that integrates settings from a JSON configuration file 
/// and a database, with support for caching to optimize performance.
/// </summary>
public class DatabaseConfigurationSource : IConfigurationSource
{
    private readonly IConfiguration _jsonConfiguration;
    private readonly string? _connectionString;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration;

    public DatabaseConfigurationSource(IServiceProvider services, IConfiguration jsonConfiguration)
    {
        _jsonConfiguration = jsonConfiguration;
        _connectionString = _jsonConfiguration.GetConnectionString(nameof(ConnectionStrings.MainConnection));
        
        // Get MemoryCache from DI
        _cache = services.GetRequiredService<IMemoryCache>();
        _cacheDuration = TimeSpan.FromMinutes(5);
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var provider = new DatabaseConfigurationProvider(_jsonConfiguration, _connectionString, _cache, _cacheDuration);
        provider.Load(); // Ensure settings are loaded into IConfiguration
        return provider;
    }
}