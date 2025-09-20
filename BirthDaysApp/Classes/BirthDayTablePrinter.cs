using BirthDaysApp.Models;
using Spectre.Console;

namespace BirthDaysApp.Classes;

/// <summary>
/// Provides functionality to print a formatted table of birthdays to the console.
/// </summary>
/// <remarks>
/// This class utilizes the Spectre.Console library to display a table of birthday records,
/// including details such as first name, last name, birthdate, and age.
/// </remarks>
public static class BirthDayTableView
{
    /// <summary>
    /// Prints a table of birthdays to the console using Spectre.Console.
    /// </summary>
    /// <param name="birthDays">
    /// A collection of <see cref="BirthDay"/> objects representing the birthdays to be displayed.
    /// </param>
    /// <remarks>
    /// The method organizes the birthdays by month and day, and displays them in a formatted table
    /// with columns for first name, last name, birthdate, and age.
    /// </remarks>
    public static void PrintBirthDays(IEnumerable<BirthDay> birthDays)
    {
        var table = new Table().Title("[cyan bold]Birthdays[/]").Border(TableBorder.Rounded).Centered();
        table.AddColumn("[cyan]First Name[/]");
        table.AddColumn("[cyan]Last Name[/]");
        table.AddColumn("[cyan]Birth Date[/]");
        table.AddColumn("[cyan]Age[/]");

        var data = birthDays.OrderBy(x => x.BirthDate.Month).ThenBy(x => x.BirthDate.Day);
        foreach (var b in data)
        {
            var birthDateText = b.BirthDate.ToString("yyyy-MM-dd", GlobalCulture.InvariantCulture);
            var ageText = (b.YearsOld ?? b.BirthDate.GetAge()).ToString(GlobalCulture.InvariantCulture);

            table.AddRow(
                b.FirstName,
                b.LastName,
                birthDateText,
                ageText
            );
        }

        AnsiConsole.Write(table);
    }
}
