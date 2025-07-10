using System.Text.Json;

namespace SectionExistsAppsettingsApp.Classes;

/// <summary>
/// Provides utility methods for working with the appsettings.json file, 
/// including functionality to check for the existence of specific properties within sections.
/// </summary>
public static class Utilities
{
    private static string FileName => "appsettings.json";

    /// <summary>
    /// Determines whether a specified property exists within a given section of the appsettings.json file.
    /// </summary>
    /// <param name="section">The name of the section in the appsettings.json file to search for.</param>
    /// <param name="propertyName">The name of the property to check for within the specified section.</param>
    /// <returns>
    /// <see langword="true"/> if the specified property exists within the given section; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="System.IO.FileNotFoundException">Thrown if the appsettings.json file is not found.</exception>
    /// <exception cref="System.Text.Json.JsonException">Thrown if the appsettings.json file contains invalid JSON.</exception>
    public static bool PropertyExists(string section, string propertyName)
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        return doc.RootElement.TryGetProperty(section, out JsonElement sectionElement) &&
               sectionElement.TryGetProperty(propertyName, out _);
    }

    /// <summary>
    /// Determines whether all specified properties exist within a given section of the appsettings.json file.
    /// </summary>
    /// <param name="section">The name of the section in the appsettings.json file to search for.</param>
    /// <param name="propertyNames">A list of property names to check for within the specified section.</param>
    /// <returns>
    /// <see langword="true"/> if all specified properties exist within the given section; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="System.IO.FileNotFoundException">Thrown if the appsettings.json file is not found.</exception>
    /// <exception cref="System.Text.Json.JsonException">Thrown if the appsettings.json file contains invalid JSON.</exception>
    /// <remarks>
    /// Generated using ChatGPT followed by Karen Payne's modifications.
    /// </remarks>
    public static bool AllPropertiesExist(string section, params string[] propertyNames)
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);

        return doc.RootElement.TryGetProperty(
            section, out JsonElement sectionElement) && 
               propertyNames.All(propertyName => sectionElement.TryGetProperty(propertyName, out _));
    }

}
