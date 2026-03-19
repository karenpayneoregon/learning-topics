namespace ConfigurationHelpersTestApp.Models;

/// <summary>
/// Represents the logging configuration settings for the application.
/// </summary>
public class LoggingSettings
{
    public Dictionary<string, string?>? LogLevel { get; set; }
}