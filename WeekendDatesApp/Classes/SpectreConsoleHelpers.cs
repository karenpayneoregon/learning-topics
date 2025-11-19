using Spectre.Console;

namespace WeekendDatesApp.Classes;
public static class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold cyan]Press any key to exit...[/]");

        Console.ReadLine();
    }

    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

}