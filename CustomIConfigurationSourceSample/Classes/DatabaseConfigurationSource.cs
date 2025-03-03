using System.Reflection;
using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace CustomIConfigurationSourceSample.Classes;

/// <summary>
/// Represents a custom configuration source that retrieves configuration data from a database.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IConfigurationSource"/> interface to provide
/// a mechanism for integrating database-stored configuration settings into the application's
/// configuration system. It supports caching to optimize performance and reduce database load.
/// </remarks>
public class DatabaseConfigurationSource : IConfigurationSource
{
    private readonly IConfigurationRoot _jsonConfiguration;
    private readonly string _connectionString;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5); // Cache for 5 minutes

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseConfigurationSource"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor sets up the configuration source by loading the application's JSON configuration file
    /// (e.g., appsettings.json) and initializing the database connection string. It also configures an in-memory
    /// cache to store configuration values for a specified duration, reducing the need for frequent database queries.
    /// </remarks>
    public DatabaseConfigurationSource()
    {
        // Load appsettings.json inside DatabaseConfigurationSource
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _jsonConfiguration = builder.Build();

        /*
         * Model is defined in ConsoleConfigurationLibrary, if using a different name
         * change the nameof(ConnectionStrings) to the name of your model class.
         */
        _connectionString = _jsonConfiguration.GetConnectionString(nameof(ConnectionStrings.MainConnection));

        // Initialize MemoryCache
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    /// <summary>
    /// Builds and returns a new instance of <see cref="IConfigurationProvider"/> 
    /// that retrieves configuration data from a database.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IConfigurationBuilder"/> instance used to construct the configuration provider.
    /// </param>
    /// <returns>
    /// An <see cref="IConfigurationProvider"/> instance that provides configuration data from the database.
    /// </returns>
    /// <remarks>
    /// This method creates a <see cref="DatabaseConfigurationProvider"/> that integrates with the 
    /// database to fetch configuration settings. It utilizes caching to improve performance 
    /// and reduce database load.
    /// </remarks>
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DatabaseConfigurationProvider(_jsonConfiguration, _connectionString, _cache, _cacheDuration);
    }

    /// <summary>
    /// Retrieves a strongly typed configuration value associated with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the configuration value to retrieve.</typeparam>
    /// <param name="key">The key of the configuration value to fetch.</param>
    /// <returns>The configuration value of type <typeparamref name="T"/>.</returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the specified key is not found in the database.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown when the value cannot be converted to the specified type <typeparamref name="T"/>.
    /// </exception>
    /// <remarks>
    /// This method first attempts to retrieve the value from the memory cache. If the value is not cached,
    /// it fetches the value from the database, converts it to the specified type, and stores it in the cache
    /// for subsequent requests.
    /// </remarks>
    public T GetValue<T>(string key)
    {
        if (_cache.TryGetValue(key, out object cachedValue))
        {
            return (T)cachedValue;
        }

        using var context = new Context(
            new DbContextOptionsBuilder<Context>()
                .UseSqlServer(_connectionString)
                .Options);

        var setting = context.ConfigurationSettings.FirstOrDefault(c => c.Key == key);
        if (setting == null) throw new KeyNotFoundException($"Key '{key}' not found in database.");

        T value = (T)Convert.ChangeType(setting.Value, typeof(T));

        // Store in cache
        _cache.Set(key, value, _cacheDuration);

        return value;
    }

    /// <summary>
    /// Removes the cached value associated with the specified key from the memory cache.
    /// </summary>
    /// <param name="key">The key of the cache entry to be invalidated.</param>
    /// <remarks>
    /// Use this method to force a refresh of a specific configuration value by removing it from the cache.
    /// The next time the value is requested, it will be fetched from the database and re-cached.
    /// </remarks>
    public void InvalidateCache(string key)
    {
        _cache.Remove(key);
    }

    /// <summary>
    /// Clears all entries from the memory cache.
    /// </summary>
    /// <remarks>
    /// This method iterates through all cache entries and removes them, effectively resetting the cache.
    /// Use this method to completely refresh the cached configuration values.
    /// </remarks>
    public void ClearCache()
    {
        var cacheKeys = _cache.GetType()
            .GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(_cache) as dynamic;

        if (cacheKeys != null)
        {
            foreach (var cacheItem in cacheKeys)
            {
                var key = cacheItem.GetType().GetProperty("Key").GetValue(cacheItem, null);
                _cache.Remove(key);
            }
        }
    }
}
