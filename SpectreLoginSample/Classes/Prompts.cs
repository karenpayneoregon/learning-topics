using Spectre.Console;
using SpectreLoginSample.Classes.Core;

namespace SpectreLoginSample.Classes;
internal class Prompts
{
    public static string PromptStyleColor { get; set; } = "cyan";
    public static string PromptColor { get; set; } = "bold";

    public static bool TryLogin(int maxAttempts = 3)
    {
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var username = GetUserName(allowEmpty: false);
            var password = GetPassword();

            if (ValidateCredentials(username, password))
            {
                AnsiConsole.MarkupLine("[green]Login successful[/]");
                return true;
            }

            /*================================================================
             * The text displays remaining attempts which is optional.
             * Showing remaining attempts can be a helpful for testing.
             ================================================================*/

            if (attempt < maxAttempts)
            {
                AnsiConsole.MarkupLine($"[red]Invalid credentials[/] - Attempts remaining: {maxAttempts - attempt} press [bold]ENTER[/] to retry");
                SpectreConsoleHelpers.ContinuePrompt();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Maximum attempts reached. Access denied.[/] press [bold]ENTER[/] to exit");
                SpectreConsoleHelpers.ContinuePrompt();
            }
            
            
        }

        return false;
    }
    /// <summary>
    /// Validates the provided username and password against predefined credentials.
    /// </summary>
    /// <param name="username">The username to validate.</param>
    /// <param name="password">The password to validate.</param>
    /// <returns>
    /// <see langword="true"/> if the credentials are valid; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method currently uses hardcoded credentials for validation. Replace this logic with
    /// actual authentication mechanisms in a production environment.
    /// </remarks>
    public static bool ValidateCredentials(string username, string password)
    {
        return username == "admin" && password == "password";
    }

    /// <summary>
    /// Get username suitable for a login
    /// </summary>
    /// <param name="allowEmpty">allows an empty string</param>
    public static string GetUserName(bool allowEmpty)
    {
        return allowEmpty
            ? AnsiConsole.Prompt(
                new TextPrompt<string>($"[{PromptColor}]User name[/]")
                    .PromptStyle(PromptStyleColor)
                    .AllowEmpty())
            : AnsiConsole.Prompt(
                new TextPrompt<string>($"[{PromptColor}]User name[/]:")
                    .PromptStyle(PromptStyleColor));
    }


    /// <summary>
    /// Get a password without exposing input
    /// </summary>
    public static string GetPassword() =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{PromptColor}]Password[/]:")
                .PromptStyle("grey50")
                .Secret()
                .DefaultValueStyle(new Style(Color.Aqua)));
}
