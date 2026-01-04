using Publisher.Models;
using System.IO;
using System.Text.Json;

namespace Publisher.Classes;

/// <summary>
/// Provides functionality to load configuration settings from a JSON file.
/// </summary>
/// <remarks>
/// The <see cref="ConfigurationLoader"/> class is responsible for reading the <c>appsettings.json</c> file,
/// deserializing its content, and returning the configuration settings as a <see cref="Configuration"/> object.
/// This class is designed to be used as a utility for loading application-specific configurations.
/// </remarks>
public static class ConfigurationLoader
{
    /// <summary>
    /// Loads the configuration settings from the <c>appsettings.json</c> file.
    /// </summary>
    /// <remarks>
    /// This method reads the <c>appsettings.json</c> file, deserializes its content into a 
    /// <see cref="Configuration"/> object, and returns the resulting configuration.
    /// </remarks>
    /// <returns>
    /// A <see cref="Configuration"/> object containing the deserialized configuration settings.
    /// </returns>
    public static Configuration Load()
    {
 
        var json = File.ReadAllText("appsettings.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var config = JsonSerializer.Deserialize<Configuration>(json, options);

  
        return config;
    }
}