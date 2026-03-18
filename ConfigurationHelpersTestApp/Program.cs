using CommonHelpersLibrary;
using ConfigurationHelpersTestApp.Classes.Core;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace ConfigurationHelpersTestApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        if (ConfigurationHelpers.PropertyExists("Logging", "LogLevel", "Microsoft.EntityFrameworkCore.Database.Command"))
        {
            /*
             * This is for asp.net core applications, but it can be used in other types
             * of applications as well.
             */
            IConfiguration configuration = ConfigurationHelpers.GetConfiguration();
            AnsiConsole.MarkupLine($"[green bold]LogLevel for Microsoft.EntityFrameworkCore.Database.Command is[/][yellow] {configuration["Logging:LogLevel:Microsoft.EntityFrameworkCore.Database.Command"]}[/]");
            
        }
        else
        {
            // Handle the case where the property does not exist
        }


        if (ConfigurationHelpers.MainConnectionExists())
        {
            SpectreConsoleHelpers.PinkPill(Justify.Left, "Main connection exists");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Main connection does not exist");
        }

        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
