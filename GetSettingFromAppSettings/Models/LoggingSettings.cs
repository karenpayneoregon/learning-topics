namespace GetSettingFromAppSettings.Models;

public class LoggingSettings
{
    public LogLevelSettings? LogLevel { get; set; } = new();
}