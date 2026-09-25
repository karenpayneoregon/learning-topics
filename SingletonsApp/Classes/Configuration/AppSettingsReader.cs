using System.Text.Json;
using SingletonsApp.Models;

namespace SingletonsApp.Classes.Configuration;

/// <summary>
/// Provides functionality to read and validate application settings from a configuration file.
/// </summary>
/// <remarks>
/// This static class is responsible for loading application settings, such as connection strings,
/// from a specified JSON file. It ensures that the required settings are present and valid.
/// </remarks>
public static class AppSettingsReader
{
    public static DataConnections Load(string path = "appsettings.json")
    {
        using FileStream stream = File.OpenRead(path);

        DataConnections settings = JsonSerializer.Deserialize<DataConnections>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new InvalidDataException($"'{path}' contains no settings.");

        if (settings.ConnectionStrings is null ||
            string.IsNullOrWhiteSpace(settings.ConnectionStrings.ProductionConnection) ||
            string.IsNullOrWhiteSpace(settings.ConnectionStrings.DevelopmentConnection) ||
            string.IsNullOrWhiteSpace(settings.ConnectionStrings.StagingConnection))
        {
            throw new InvalidDataException($"'{path}' is missing required connection strings.");
        }

        return settings;
    }
}
