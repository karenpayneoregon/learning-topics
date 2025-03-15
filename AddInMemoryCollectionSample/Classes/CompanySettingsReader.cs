using System.Text.Json;

namespace AddInMemoryCollectionSample.Classes;

/// <summary>
/// The idea here is that a company has a global configuration file that contains settings for all applications.
/// Here the json file is in the root of the project and is named companysettings.json but for real projects
/// companysettings.json would be in a location accessible to all applications or there could be a MS build
/// event that copies the companysettings.json from a standard location.
/// </summary>
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