using PrintMembersSamples.Models;


namespace PrintMembersSamples.Classes;

public static class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();

        Render(new Rule($"[yellow]Press[/] [cyan]ENTER[/] [yellow]to exit the demo[/]")
            .RuleStyle(Style.Parse("silver")).LeftJustified());

        Console.ReadLine();
    }

    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    /// Enhances the string representation of a <see cref="Person"/> object
    /// by applying color formatting to its property names using NuGet package Spectre.Console.
    /// </summary>
    /// <param name="person">The <see cref="Person"/> instance to be colorized.</param>
    /// <returns>A string representation of the <paramref name="person"/> object
    /// with colorized property names.</returns>
    public static string Colorize(this Person person) =>
        person.ToString()
            .Replace("FirstName", "[cyan]FirstName[/]")
            .Replace("LastName", "[cyan]LastName[/]")
            .Replace("Birth", "[cyan]Birth[/]")
            .Replace("SSN", "[cyan]SSN[/]")
            .Replace("PhoneNumbers", "[cyan]Phone Numbers[/]");
    
    /// <summary>
    /// Applies color formatting to the properties of a <see cref="Person"/> object and masks the Social Security Number (SSN).
    /// </summary>
    /// <param name="person">The <see cref="Person"/> object whose properties will be colorized and whose SSN will be masked.</param>
    /// <returns>
    /// A string representation of the <see cref="Person"/> object with colorized property names and a masked SSN.
    /// </returns>
    /// <remarks>
    /// The method replaces the property names (e.g., "FirstName", "LastName", etc.) with their colorized versions
    /// and masks the SSN using the <see cref="StringExtensions.MaskSsn"/> method.
    /// </remarks>
    public static string ColorizeMaskSsn(this Person person) =>
        person.ToString()
            .Replace("FirstName", "[cyan]FirstName[/]")
            .Replace("LastName", "[cyan]LastName[/]")
            .Replace("Birth", "[cyan]Birth[/]")
            .Replace(person.SSN, person.SSN.MaskSsn())
            .Replace("SSN", "[cyan]SSN[/]")
            .Replace("PhoneNumbers", "[cyan]Phone Numbers[/]");

}