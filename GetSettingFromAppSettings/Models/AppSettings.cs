namespace GetSettingFromAppSettings.Models;

public class AppSettings
{
    public LoggingSettings Logging { get; set; } = new();
}