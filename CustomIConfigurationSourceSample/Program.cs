using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceSample.Classes;
using CustomIConfigurationSourceSample.Models;
using Microsoft.Extensions.Configuration;

namespace CustomIConfigurationSourceSample;

internal partial class Program
{
    static void Main(string[] args)
    {

        AnsiConsole.MarkupLine("[cyan]Connection string[/]");
        AnsiConsole.MarkupLine($"[yellow]{AppConfiguration.Instance.MainConnection}[/]");
        Console.WriteLine();
        AnsiConsole.MarkupLine("[cyan]Help desk[/]");
        AnsiConsole.MarkupLine($"[white]Phone:[/] [yellow]{AppConfiguration.Instance.HelpDesk.Phone}[/]");
        AnsiConsole.MarkupLine($"[white]Email:[/] [yellow]{AppConfiguration.Instance.HelpDesk.Email}[/]");

        Console.ReadLine();
    }
}