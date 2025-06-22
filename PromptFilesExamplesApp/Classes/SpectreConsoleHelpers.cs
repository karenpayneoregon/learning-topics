using System.Runtime.CompilerServices;


namespace PromptFilesExamplesApp.Classes;
public static class SpectreConsoleHelpers
{
    /// <summary>
    /// Creates a pre-configured <see cref="Spectre.Console.Table"/> instance
    /// with columns for customer details such as First Name, Last Name, Gender,
    /// Birth Date, and Email.
    /// </summary>
    /// <returns>
    /// A <see cref="Spectre.Console.Table"/> object with a rounded border, 
    /// left-aligned content, and a title of "Customers".
    /// </returns>
    /// <remarks>
    /// The table is styled with a light slate grey border color and uses 
    /// square borders. Column headers are styled with cyan text.
    /// </remarks>
    public static Table CreateTable()
        => new Table().RoundedBorder()
            .AddColumn("[cyan]First[/]")
            .AddColumn("[cyan]Last[/]")
            .AddColumn("[cyan]Gender[/]")
            .AddColumn("[cyan]Birth[/]")
            .AddColumn("[cyan]Email[/]")
            .Alignment(Justify.Left)
            .BorderColor(Color.LightSlateGrey)
            .Border(TableBorder.Square)
            .Title("[LightGreen]Customers[/]");

    /// <summary>
    /// Outputs the name of the calling method to the console in cyan color.
    /// </summary>
    /// <param name="methodName">
    /// The name of the calling method. This parameter is optional and will be
    /// automatically populated with the caller's method name if not explicitly provided.
    /// </param>
    /// <remarks>
    /// This method uses <see cref="AnsiConsole.MarkupLine(string)"/> to format
    /// the output text in cyan color, providing a visual cue for method execution.
    /// </remarks>
    public static void PrintCyan([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{methodName}[/]");
        Console.WriteLine();
    }
}
