using System.Text.Json;
using SingletonLibrary.Models;

namespace SingletonLibrary.Classes;

/// <summary>
/// Provides functionality to read and validate application settings from a configuration file.
/// </summary>
/// <remarks>
/// This static class is responsible for loading application settings, such as connection strings,
/// from a specified JSON file. It ensures that the required settings are present and valid.
/// </remarks>
public static class AppSettingsReader
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNameCaseInsensitive = true };

    /// <summary>
    /// Loads the connection strings configuration from a specified JSON file.
    /// </summary>
    /// <param name="path">
    /// The path to the JSON configuration file. Defaults to "appsettings.json" if not specified.
    /// </param>
    /// <returns>
    /// An instance of <see cref="Models.DataConnections"/> containing the deserialized connection strings.
    /// </returns>
    /// <exception cref="System.IO.FileNotFoundException">
    /// Thrown if the specified file does not exist.
    /// </exception>
    /// <exception cref="System.Text.Json.JsonException">
    /// Thrown if the JSON file is invalid or cannot be deserialized into a <see cref="Models.DataConnections"/> object.
    /// </exception>
    /// <remarks>
    /// This method reads the content of the specified JSON file, deserializes it into a <see cref="Models.DataConnections"/> object,
    /// and returns the resulting instance. It uses case-insensitive property matching during deserialization.
    /// </remarks>
    public static DataConnections LoadConnectionStringsConnections(string path = "appsettings.json") 
        => JsonSerializer.Deserialize<DataConnections>(File.ReadAllText(path), SerializerOptions)!;

   
}

