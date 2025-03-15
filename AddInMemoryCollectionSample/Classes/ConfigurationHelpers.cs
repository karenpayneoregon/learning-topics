using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AddInMemoryCollectionSample.Classes;

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

public class CompanySettingsReader
{
    /// <summary>
    /// Reads company settings from a JSON file and converts them into a list of key-value pairs
    /// suitable for in-memory configuration.
    /// </summary>
    /// <remarks>
    /// The method reads a JSON file named "companysettings.json", deserializes its content into a 
    /// dictionary, and processes nested objects into a flattened key-value structure. This is useful 
    /// for scenarios where configuration data needs to be loaded dynamically into an in-memory 
    /// configuration provider.
    /// </remarks>
    /// <returns>
    /// A list of key-value pairs representing the company settings, where nested objects are 
    /// flattened into colon-separated keys.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the "companysettings.json" file is not found.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown if the JSON content cannot be deserialized.
    /// </exception>
    public static List<KeyValuePair<string, string>> CompanySettingsList()
    {
        string filePath = "companysettings.json";

        string json = File.ReadAllText(filePath);

        // Deserialize JSON into a Dictionary<string, object>
        var settings = JsonSerializer.Deserialize<Dictionary<string, object>>(json, Options);

        // Convert settings to key-value pairs for in-memory configuration
        var configData = new List<KeyValuePair<string, string>>();

        foreach (var key in settings)
        {
            if (key.Value is JsonElement { ValueKind: JsonValueKind.Object } element)
            {
                var nestedDict = JsonSerializer.Deserialize<Dictionary<string, string>>(element.GetRawText());
                configData.AddRange(nestedDict.Select(nestedKey => new KeyValuePair<string, string>($"{key.Key}:{nestedKey.Key}", nestedKey.Value)));
            }
            else
            {
                configData.Add(new KeyValuePair<string, string>(key.Key, key.Value?.ToString() ?? ""));
            }
        }

        return configData;
    }

    public static JsonSerializerOptions Options => new() { PropertyNameCaseInsensitive = true };
}