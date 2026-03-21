namespace GetSettingFromAppSettings.Models;
public class LogLevelSettings
{
    public string? Default { get; set; } = string.Empty;

    [ConfigurationKeyName("Microsoft.AspNetCore")]
    public string? MicrosoftAspNetCore { get; set; } = string.Empty;

    [ConfigurationKeyName("Microsoft.EntityFrameworkCore.Database.Command")]
    public string? MicrosoftEntityFrameworkCoreDatabaseCommand { get; set; } = string.Empty;
}