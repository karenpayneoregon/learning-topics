# About
<!-- TOC-->
  - [appsettings](#appsettings)
  - [Loosely coupled code for Logging section](#loosely-coupled-code-for-logging-section)
  - [Strongly typed code for Logging section](#strongly-typed-code-for-logging-section)
<!-- TOC -->
The intent of the code is to demonstrate how to check whether sections and keys exist and retrieve key values from appsettings.json. Some of the code is dependency injection ready while the majority is for desktop applications.

Depend on `CommonHelpersLibrary.ConfigurationHelpers.`

Usually, you would want to use strongly typed classes to represent your configuration sections such as for the Logging section but there may be cases where you want to check for the existence of a key or section and retrieve its value without using strongly typed classes.

```csharp
public class LogLevelSettings
{
    public string? Default { get; set; }

    [ConfigurationKeyName("Microsoft.AspNetCore")]
    public string? MicrosoftAspNetCore { get; set; }

    [ConfigurationKeyName("Microsoft.EntityFrameworkCore.Database.Command")]
    public string? MicrosoftEntityFrameworkCoreDatabaseCommand { get; set; }
}

public class AppSettings
{
    public LoggingSettings? Logging { get; set; }
}

public class LoggingSettings
{
    public Dictionary<string, string?>? LogLevel { get; set; }
}
```

## appsettings
```json
{
  "ConnectionStrings": {
    "MainConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=AppsettingsConfigurations;Integrated Security=True;Encrypt=False"
  },
  "EntityConfiguration": {
    "CreateNew": true
  },
  "Logging": {
    "LogLevel": {
      "Microsoft.EntityFrameworkCore.Database.Command": "Information",
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Loosely coupled code for Logging section

Useful for quick checks without needing to create strongly typed classes. Also useful for desktop applications where you may not want to set up dependency injection.

```csharp
using static CommonHelpersLibrary.ConfigurationHelpers;

internal partial class Program
{
    static void Main(string[] args)
    {

        IConfiguration configuration = GetConfiguration();
        var p1 = "Logging:LogLevel:Microsoft.EntityFrameworkCore.Database.Command";

        if (PropertyExists("Logging", "LogLevel", "Microsoft.EntityFrameworkCore.Database.Command"))
        {
            /*
             * This is for asp.net core applications, but it can be used in other types
             * of applications as well.
             */
            AnsiConsole.MarkupLine($"[green bold]LogLevel for {p1} =[/][yellow] {configuration[p1]}[/]");

        }
        else
        {
            SpectreConsoleHelpers.InfoPill(Justify.Left, $"LogLevel for {p1} not found.");
        }

        Console.WriteLine();

        var p2 = "Logging:LogLevel:Microsoft.AspNetCore";
        if (PropertyExists("Logging", "LogLevel", "Microsoft.AspNetCore"))
        {
            AnsiConsole.MarkupLine($"[green bold]LogLevel for {p2} =[/][yellow] {configuration[p2]}[/]");
        }

        Console.WriteLine();

        StrongTypedExamples(configuration);


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
```

## Strongly typed code for Logging section

Recommended for ASP.NET Core applications and desktop applications where you want to use strongly typed classes to represent your configuration sections. This allows for better maintainability and easier access to configuration values.

```csharp
private static void StrongTypedExamples(IConfiguration configuration)
{
    SpectreConsoleHelpers.PrintPink();

    if (!configuration.GetSection("Logging").Exists())
    {
        AnsiConsole.MarkupLine($"[red bold]Logging section does not exist.[/]");
        return;
    }

    var appSettings = new AppSettings();
    configuration.Bind(appSettings);
   
    var logLevelSettings = configuration.GetSection("Logging:LogLevel").Get<LogLevelSettings>();
    if (logLevelSettings != null)
    {
        AnsiConsole.MarkupLine($"[green bold]Default LogLevel =[/][yellow] {logLevelSettings.Default}[/]");
        AnsiConsole.MarkupLine($"[green bold]ASP.NET Core LogLevel =[/][yellow] {logLevelSettings.MicrosoftAspNetCore}[/]");
        AnsiConsole.MarkupLine($"[green bold]EF Core Command LogLevel =[/][yellow] {logLevelSettings.MicrosoftEntityFrameworkCoreDatabaseCommand}[/]");
    }

    Console.WriteLine();

    // Example of checking for a specific connection string
    if (MainConnectionExists())
    {
        AnsiConsole.MarkupLine($"[green bold]Main connection exists:[/][yellow] {AppConnections.Instance.MainConnection}[/]");

    }
    else
    {
        SpectreConsoleHelpers.ErrorPill(Justify.Left, "Main connection does not exist");
    }
}
```