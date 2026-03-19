using Microsoft.Extensions.Configuration;
using System.Text.Json;

// See project ConfigurationHelpersTestApp for example usage of this class and its methods.

namespace CommonHelpersLibrary;
/// <summary>
/// Provides utility methods for validating the presence of specific sections 
/// in the "appsettings.json" configuration file.
/// </summary>
/// <remarks>
/// This class is designed to assist in ensuring that critical configuration 
/// sections, such as "EntityConfiguration" and "ConnectionStrings", are  present in
/// the application's configuration file. These checks are  essential for maintaining
/// the integrity of application setup and  preventing runtime errors due to missing configurations.
/// </remarks>
public class ConfigurationHelpers
{
    /// <summary>
    /// Gets the name of the configuration file used for validating the presence of specific sections.
    /// </summary>
    /// <value>
    /// The name of the configuration file, which is "appsettings.json" or this can be changed to a development version.
    /// </value>
    /// <remarks>
    /// This property provides the default file name for the application's configuration file, 
    /// which is utilized by methods in the <see cref="ConfigurationHelpers"/> class to perform validation checks.
    /// </remarks>
    private static string FileName => "appsettings.json";

    /// <summary>
    /// Determines whether the "EntityConfiguration" section exists in the "appsettings.json" file.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the "EntityConfiguration" section exists; otherwise, <c>false</c>.
    /// </returns>
    public static bool EntityConfigurationSectionExists()
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);

        return doc.RootElement.TryGetProperty("EntityConfiguration", out _);
    }

    /// <summary>
    /// Determines whether the "ConnectionStrings" section exists in the "appsettings.json" file.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the "ConnectionStrings" section exists; otherwise, <c>false</c>.
    /// </returns>
    public static bool ConnectionStringsSectionExists()
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);

        return doc.RootElement.TryGetProperty("ConnectionStrings", out _);
    }

    /// <summary>
    /// Checks if the "MainConnection" entry exists within the "ConnectionStrings" section
    /// of the "appsettings.json" configuration file and retrieves its value.
    /// </summary>
    /// <returns>
    /// A tuple containing two elements:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <c>exists</c>: A <see cref="bool"/> indicating whether the "MainConnection" entry exists.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <c>connectionString</c>: A <see cref="string"/> representing the value of the "MainConnection" entry,
    /// or <c>null</c> if the entry does not exist.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// This method is designed to validate the presence of a critical database connection string
    /// in the application's configuration file. It ensures that the "MainConnection" entry is available
    /// and retrieves its value for further use in the application.
    /// </remarks>
    public static (bool exists, string? connectionString) MainConnectionExists()
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);

        if (doc.RootElement.TryGetProperty("ConnectionStrings", out JsonElement connectionStrings) &&
            connectionStrings.TryGetProperty("MainConnection", out JsonElement cnElement))
        {
            // Prefer GetString for string values; fallback to ToString if necessary.
            string? connectionString = cnElement.ValueKind == JsonValueKind.String
                ? cnElement.GetString()
                : cnElement.ToString();

            return (true, connectionString);
        }

        return (false, null);
    }


    public static bool PropertyExists(string section, string propertyName)
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        return doc.RootElement.TryGetProperty(section, out JsonElement sectionElement) &&
               sectionElement.TryGetProperty(propertyName, out _);
    }

    /// <summary>
    /// Determines whether a property exists in the "appsettings.json" file based on a specified path.
    /// </summary>
    /// <param name="path">
    /// An array of strings representing the hierarchical path to the property. Each element in the array 
    /// corresponds to a level in the JSON structure.
    /// </param>
    /// <returns>
    /// <c>true</c> if the property exists at the specified path; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method allows for flexible and dynamic checks of deeply nested properties within the 
    /// "appsettings.json" file. It traverses the JSON structure based on the provided path and verifies 
    /// the existence of the specified property.
    /// </remarks>
    public static bool PropertyExists(params string[] path)
    {
        string jsonContent = File.ReadAllText(FileName);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);

        JsonElement current = doc.RootElement;

        return path.All(segment 
            => current.ValueKind == JsonValueKind.Object && 
               current.TryGetProperty(segment, out current));
    }

    /// <summary>
    /// Checks whether a specific property exists in the provided configuration.
    /// </summary>
    /// <param name="configuration">
    /// The <see cref="IConfiguration"/> instance representing the application's configuration.
    /// </param>
    /// <param name="key">
    /// The key of the property to check for existence.
    /// </param>
    /// <returns>
    /// <c>true</c> if the specified property exists in the configuration; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="configuration"/> parameter is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="key"/> parameter is <c>null</c>, empty, or consists only of whitespace.
    /// </exception>
    public static bool PropertyExists(IConfiguration configuration, string key)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        return configuration.GetSection(key).Exists();
    }

    /// <summary>
    /// Attempts to retrieve a value of the specified type from the configuration 
    /// using the provided key.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value to retrieve from the configuration.
    /// </typeparam>
    /// <param name="configuration">
    /// The configuration instance to search for the key. Must not be <c>null</c>.
    /// </param>
    /// <param name="key">
    /// The key of the configuration entry to retrieve. Must not be <c>null</c> or empty.
    /// </param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, 
    /// if the key is found and the value can be converted to the specified type; 
    /// otherwise, the default value for the type of the <typeparamref name="T"/> parameter.
    /// </param>
    /// <returns>
    /// <c>true</c> if the key exists and the value is successfully retrieved; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="configuration"/> parameter is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="key"/> parameter is <c>null</c>, empty, or consists only of white-space characters.
    /// </exception>
    public static bool TryGetValue<T>(IConfiguration configuration, string key, out T value)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        var section = configuration.GetSection(key);

        if (!section.Exists())
        {
            value = default!;
            return false;
        }

        value = section.Get<T>()!;
        return true;
    }

    /// <summary>
    /// For demonstration purposes only.
    /// Builds and retrieves the application's configuration from the "appsettings.json" file.
    /// </summary>
    /// <returns>
    /// An <see cref="IConfiguration"/> instance representing the application's configuration.
    /// </returns>
    /// <remarks>
    /// This method initializes a configuration builder, sets the base path to the current 
    /// directory, and loads the "appsettings.json" file. The configuration is set to reload 
    /// automatically if the file changes.
    /// </remarks>
    public static IConfiguration GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var name =  Path.GetFileNameWithoutExtension(FileName);
        
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(FileName, optional: false, reloadOnChange: true)
            .AddJsonFile($"{name}.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
