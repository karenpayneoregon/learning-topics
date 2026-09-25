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
    private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public static DataConnections LoadConnectionStringsConnections(string path = "appsettings.json") 
        => JsonSerializer.Deserialize<DataConnections>(File.ReadAllText(path), SerializerOptions)!;

    public static HelpDesk LoadHelpDesk(string path = "appsettings.json") 
        => JsonDocument.Parse(File.ReadAllText(path))
            .RootElement.GetProperty(nameof(HelpDesk))
            .Deserialize<HelpDesk>(SerializerOptions)!;
}

