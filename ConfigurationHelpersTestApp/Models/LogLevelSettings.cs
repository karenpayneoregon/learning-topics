using Microsoft.Extensions.Configuration;

namespace ConfigurationHelpersTestApp.Models;
/// <summary>
/// Represents the log level settings for various components in the application.
/// </summary>
/// <remarks>
/// This class is designed to map to the "Logging:LogLevel" section in the application's configuration.
/// It provides properties for specifying log levels for default logging, ASP.NET Core, and Entity Framework Core database commands.
/// </remarks>
public class LogLevelSettings
{
    public string? Default { get; set; }

    [ConfigurationKeyName("Microsoft.AspNetCore")]
    public string? MicrosoftAspNetCore { get; set; }

    [ConfigurationKeyName("Microsoft.EntityFrameworkCore.Database.Command")]
    public string? MicrosoftEntityFrameworkCoreDatabaseCommand { get; set; }
}