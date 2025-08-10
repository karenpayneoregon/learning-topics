using Spectre.Console;
using System.Runtime.CompilerServices;
using Serilog;

namespace WineConsoleApp.Classes;
public static class AnsiConsoleHelpers
{
    /// <summary>
    /// Write text with foreground color cyan
    /// </summary>
    /// <param name="text">What to display</param>
    public static void CyanMarkup(string text)
    {
        AnsiConsole.MarkupLine($"[cyan]{text}[/]");
    }

    public static void PrintCyan([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{methodName}[/]");
        Log.Information($"Starting {methodName}");
        Console.WriteLine();
    }
    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    public static void ExitPrompt()
    {
        Console.WriteLine();
        Render(new Rule($"[yellow]Press a key to exit the demo[/]").RuleStyle(Style.Parse("silver")).Centered());
        Console.ReadLine();
    }

}
