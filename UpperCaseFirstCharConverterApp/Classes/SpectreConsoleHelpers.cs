using Spectre.Console;
using Spectre.Console.Json;

namespace UpperCaseFirstCharConverterApp.Classes;
public static class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();
        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold cyan]Press ENTER key to exit...[/]");

        Console.ReadLine();
    }
    /// <summary>
    /// Displays a JSON string in a formatted and color-coded manner using Spectre.Console.
    /// </summary>
    /// <param name="json">The JSON string to be presented.</param>
    /// <remarks>
    /// The method uses Spectre.Console's <see cref="JsonText"/> to render the JSON with specific colors for braces, brackets, colons, commas, strings, numbers, booleans, members, and null values.
    /// </remarks>
    public static void PresentJson(string json)
    {
        AnsiConsole.Write(
            new JsonText(json)
                .BracesColor(Color.Red)
                .BracketColor(Color.Green)
                .ColonColor(Color.White)
                .CommaColor(Color.Cyan1)
                .StringColor(Color.GreenYellow)
                .NumberColor(Color.White)
                .BooleanColor(Color.Red)
                .MemberColor(Color.Yellow)
                .NullColor(Color.Green));
    }
    
    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
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