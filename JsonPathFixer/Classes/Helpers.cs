using System.Text.Json;

namespace JsonPathFixer.Classes;

public static class Helpers
{
    /// <summary>
    /// Formats a given file path for embedding into a JSON string.
    /// Escapes backslashes and wraps the result in double quotes.
    /// </summary>
    /// <param name="path">The UNC or file path to format.</param>
    /// <returns>A JSON-safe string with escaped characters.</returns>
    public static string FormatPathForJson(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));


        return JsonSerializer.Serialize(path);  

    }

    /// <summary>
    /// Prompts the user to enter a file path and retrieves the input.
    /// </summary>
    /// <returns>The file path entered by the user as a string.</returns>
    public static string GetPath() =>
        AnsiConsole.Prompt(
            new TextPrompt<string>("[white]Enter path to format[/]")
                .PromptStyle("cyan")
                .ValidationErrorMessage("[red]Enter path to format[/]"));
}
