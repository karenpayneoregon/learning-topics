namespace ConfigurationHelpersTestApp.Models;

/// <summary>
/// Represents the application settings configuration.
/// </summary>
/// <remarks>
/// This class is used to bind and access strongly-typed configuration settings
/// for the application, such as logging settings.
/// </remarks>
public class AppSettings
{
    public LoggingSettings? Logging { get; set; }
}