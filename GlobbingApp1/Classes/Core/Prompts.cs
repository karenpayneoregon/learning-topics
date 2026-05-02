using Spectre.Console;

namespace GlobbingApp1.Classes.Core;

/// <summary>
/// Examples of various prompts using Spectre.Console to capture user input with validation and styling.
/// </summary>
internal class Prompts
{

    private static readonly Style Style = new(Color.Fuchsia, Color.Black, Decoration.None);

    /// <summary>
    /// Prompts the user to enter an integer with the specified prompt text.
    /// </summary>
    /// <param name="prompt">
    /// The text to display as the prompt message. Defaults to "Enter an integer".
    /// </param>
    /// <returns>
    /// The user's input as an integer.
    /// </returns>
    /// <remarks>
    /// This method uses a styled prompt to request an integer input from the user.
    /// If the input is invalid, an error message is displayed, and the user is prompted again.
    /// </remarks>
    public static int GetInt(string prompt = "Enter an integer") =>
        AnsiConsole.Prompt(
            new TextPrompt<int>($"[cyan]{prompt}[/]")
                .PromptStyle("yellow")
                .DefaultValue(0)
                .DefaultValueStyle(Style));

    public static decimal GetDecimal(string prompt = "Enter a decimal") =>
        AnsiConsole.Prompt(
            new TextPrompt<decimal>($"[cyan]{prompt}[/]")
                .PromptStyle("yellow")
                .DefaultValue(1.0m)
                .DefaultValueStyle(Style));

    private static string promptColor => "[cyan]";
    private static string inputColor => "white";
    
    /// <summary>
    /// Prompts the user to enter a password with the specified prompt text.
    /// </summary>
    /// <param name="text">The text to display as the prompt message.</param>
    /// <returns>
    /// The user's input as a string. The input is treated as a secret and can be empty.
    /// </returns>
    /// <remarks>
    /// This method uses a secret input prompt to securely capture the user's password or sensitive information.
    /// The prompt text is styled with a predefined color for better visual distinction.
    /// </remarks>
    public static string SecretPrompt(string text) => AnsiConsole.Prompt(
        new TextPrompt<string>($"Enter {promptColor}{text}[/]?")
            .PromptStyle(inputColor)
            .Secret()
            .AllowEmpty());

    public static DateOnly? GetDate(string text = "Enter a date") =>
        AnsiConsole.Prompt(new TextPrompt<DateOnly>($"Enter [white]{text}[/]")
            .PromptStyle("yellow")
            .ValidationErrorMessage("[red]Please enter a valid date or press ENTER to not enter a date[/]")
            .AllowEmpty());

    /// <summary>
    /// Prompts the user to enter their birthdate and validates the input.
    /// </summary>
    /// <returns>
    /// A <see cref="DateOnly"/>? representing the user's birthdate if entered, or <c>null</c> if no date is provided.
    /// </returns>
    /// <remarks>
    /// This method displays a prompt asking for the user's birthdate. The input is validated to ensure
    /// that the year is less than 2000. If the input is invalid, an error message is displayed, and the user
    /// is prompted again. The user can also choose not to enter a date by pressing ENTER.
    /// </remarks>
    public static DateOnly? GetBirthDate()
        {
            const int birthYear = 2000;
            
            DateOnly? result = AnsiConsole.Prompt(
                new TextPrompt<DateOnly>("What is your [white]birth date[/]?")
                    .PromptStyle("yellow")
                    .ValidationErrorMessage("[red]Please enter a valid date or press ENTER to not enter a date[/]")
                    .Validate(dateOnly => dateOnly.Year switch
                    {
                        >= birthYear => ValidationResult.Error($"[red]Must be less than {birthYear}[/]"),
                        _ => ValidationResult.Success(),
                    })
                    .AllowEmpty());

            return result;
        }

    public static List<string> QuestionOptions => ["Y", "N"];

    /// <summary>
    /// Prompts the user with a question and validates the response against predefined options.
    /// </summary>
    /// <param name="questionText">The text of the question to display to the user.</param>
    /// <param name="color">The color to use for the question text.</param>
    /// <returns>
    /// The user's response as a string, validated against the predefined options.
    /// </returns>
    /// <remarks>
    /// This method displays a prompt with customizable text and color, and validates the user's input 
    /// against a predefined set of options (<see cref="QuestionOptions"/>). If the input is invalid, 
    /// an error message is displayed, and the user is prompted again.
    /// </remarks>
    public static string Continue(string questionText, string color) =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{color}]{questionText}[/] {string.Join(",", QuestionOptions)}")
                .PromptStyle("cyan")
                .DefaultValue("y")
                .ValidationErrorMessage($"[red]Valid responses[/] [white]{string.Join(",", QuestionOptions)}[/] [red]or press ENTER for default[/]")
                .Validate(text => QuestionOptions.Contains(text, StringComparer.CurrentCultureIgnoreCase) switch
                {
                    false => ValidationResult.Error("[red]Must be[/] [yellow]y[/] [red]or[/] [yellow]n[/]"),
                    _ => ValidationResult.Success()
                }));

    /// <summary>
    /// Prompts the user with a yes/no question and returns a boolean response.
    /// </summary>
    /// <param name="questionText">The text of the question to display to the user.</param>
    /// <param name="color">The color to use for the question text. Defaults to "white".</param>
    /// <returns>
    /// <c>true</c> if the user responds with "Y" (case-insensitive); otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method allows customization of the text color for the (y/n) prompt, 
    /// unlike <see cref="AnsiConsole.Confirm"/> which does not support color customization.
    /// </remarks>
    public static bool Question(string questionText, string color = "white") 
        => Continue(questionText, color)
            .ToUpper()
            .Equals("Y");

    /// <summary>
    /// Prompts the user for input of a specified type and returns the entered value.
    /// </summary>
    /// <typeparam name="T">The type of the value to be entered by the user.</typeparam>
    /// <param name="prompt">The text of the prompt to display to the user.</param>
    /// <param name="defaultValue">The default value to return if the user provides no input.</param>
    /// <returns>The value entered by the user, or the <paramref name="defaultValue"/> if no input is provided.</returns>
    /// <remarks>
    /// This method uses a generic type parameter to allow flexible input types and provides a default value
    /// for cases where the user does not enter any input. Validation and styling are applied to enhance the user experience.
    /// </remarks>
    public static T Get<T>(string prompt, T defaultValue) =>
        AnsiConsole.Prompt(
            new TextPrompt<T>($"[white]{prompt}[/]")
                .PromptStyle("white")
                .DefaultValueStyle(new(Color.Yellow))
                .DefaultValue(defaultValue)
                .ValidationErrorMessage("[white on red]Invalid entry![/]"));
}


