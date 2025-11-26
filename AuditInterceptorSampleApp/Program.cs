using System.Text.Json;
using AuditInterceptorSampleApp.Classes;
using AuditInterceptorSampleApp.Classes.Configurations;
using AuditInterceptorSampleApp.Data;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace AuditInterceptorSampleApp;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        AnsiConsole.MarkupLine("[cyan]Creating database[/]");

        var (success, exception) = await SetupDatabase.CreateDatabase();
        if (success)
        {
            AnsiConsole.MarkupLine("[cyan]Performing updates[/]");
            await DataOperations.UpdateRecords();
            AnsiConsole.MarkupLine("[cyan]Done, check out the log file under[/] [yellow]LogFiles[/] [cyan]from the app folder[/]");
            AnsiConsole.MarkupLine($"[orchid]{FileHelper.GetLogFileName()}[/]");
            
            

        }
        else
        {
            AnsiConsole.WriteException(exception);
        }

        SpectreConsoleHelpers.ExitPrompt();
    }


}