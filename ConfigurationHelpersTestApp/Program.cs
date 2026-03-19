using ConfigurationHelpersTestApp.Classes.Core;
using ConfigurationHelpersTestApp.Models;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using static CommonHelpersLibrary.ConfigurationHelpers;

namespace ConfigurationHelpersTestApp;
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
}


