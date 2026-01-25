using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SpectreConsoleMenuApp.Models;

namespace SpectreConsoleMenuApp.Classes.Utilities;

public class NextValueService
{
    private readonly IConfiguration _configuration;
    private readonly string _configPath;

    /// <summary>
    /// Initializes a new instance of the <see cref="NextValueService"/> class.
    /// </summary>
    /// <param name="configuration">
    /// An instance of <see cref="IConfiguration"/> used to access application settings.
    /// </param>
    /// <remarks>
    /// This constructor sets up the service by assigning the provided configuration instance 
    /// and determining the path to the application's settings file.
    /// </remarks>
    public NextValueService(IConfiguration configuration)
    {
        _configuration = configuration;
        _configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    }

    /// <summary>
    /// Retrieves the current PIN value from the application configuration.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> representing the current PIN value, or <c>null</c> if the value is not found.
    /// </returns>
    /// <remarks>
    /// This method accesses the "NextValue:Pin" key in the application's configuration file to retrieve the PIN.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    /// var nextValueService = new NextValueService(configuration);
    /// string? currentPin = nextValueService.ReadPin();
    /// Console.WriteLine($"Current PIN: {currentPin}");
    /// </code>
    /// </example>
    public string? ReadPin()
    {
        return _configuration["NextValue:Pin"];
    }

    /// <summary>
    /// Updates the PIN value in the application's settings file.
    /// </summary>
    /// <param name="newPin">The new PIN value to be saved.</param>
    /// <remarks>
    /// This method reads the current application settings from the configuration file, 
    /// updates the <c>NextValue:Pin</c> property with the provided <paramref name="newPin"/>, 
    /// and writes the updated settings back to the file in a formatted JSON structure.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the configuration file specified by the path <c>_configPath</c> does not exist.
    /// </exception>
    /// <exception cref="JsonException">
    /// Thrown if the configuration file contains invalid JSON or cannot be deserialized into the expected structure.
    /// </exception>
    /// <example>
    /// Example usage:
    /// <code>
    /// var nextValueService = new NextValueService(configuration);
    /// nextValueService.UpdatePin("A1B2C24");
    /// </code>
    /// </example>
    public void UpdatePin(string newPin)
    {
        var json = File.ReadAllText(_configPath);

        var settings = JsonSerializer.Deserialize<AppSettings>(json);
        settings.NextValue.Pin = newPin;

        var updatedJson = JsonSerializer.Serialize(
            settings,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_configPath, updatedJson);
    }
}