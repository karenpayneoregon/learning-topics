namespace GetSettingFromAppSettings.Models;

/// <summary>
/// Represents the logging settings configuration section.
/// </summary>
/// <remarks>
/// This class is used to bind the "Logging" section of the application's configuration.
/// It contains properties that define the logging levels for various components of the application.
/// </remarks>
public class LoggingSettings
{
    public LogLevelSettings? LogLevel { get; set; } = new();
}