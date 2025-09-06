using Spectre.Console;
using System.Runtime.CompilerServices;

namespace PartialSamples1.Classes.Configurations;
public static class SpectreConsoleHelpers
{
    /// <summary>
    /// Displays a prompt in the console indicating the user should press any key to exit the application.
    /// </summary>
    /// <remarks>
    /// This method uses Spectre.Console to format the prompt message and waits for user input via <see cref="Console.ReadLine"/>.
    /// </remarks>
    public static void ExitPrompt()
    {
        AnsiConsole.MarkupLine("[bold mediumpurple2]Press any key to exit...[/]");

        Console.ReadLine();
    }

    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    public static void PrintCyan([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{methodName}[/]");
        Console.WriteLine();
    }


    /// <summary>
    /// Spectre.Console  Add [ to [ and ] to ] so Children[0].Name changes to Children[[0]].Name
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string ConsoleEscape(this string sender)
        => Markup.Escape(sender);

    /// <summary>
    /// Spectre.Console Removes markup from the specified string.
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static string ConsoleRemove(this string sender)
        => Markup.Remove(sender);
}