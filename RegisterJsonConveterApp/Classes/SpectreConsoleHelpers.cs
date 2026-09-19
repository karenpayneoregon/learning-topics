using Spectre.Console;
using Spectre.Console.Json;
using System.Runtime.CompilerServices;
using System.Text;
using RegisterJsonConveterApp.Classes;

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

    public static void PrintPink([CallerFilePath] string? filePath = null, [CallerMemberName] string? methodName = null)
    {

        // Get file and project name
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var projectName = Utilities.GetProjectName(filePath);

        AnsiConsole.MarkupLine($"[hotpink2]{projectName}[/][yellow bold].[/][hotpink2]" +
                               $"{fileName}[/][yellow bold].[/][hotpink2]{methodName}[/]");

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

    public static void SetEncoding()
    {
        Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        Console.InputEncoding = Encoding.UTF8;
    }
}