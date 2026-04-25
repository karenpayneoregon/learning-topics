using CommonHelpersLibrary;
using CommonHelpersLibrary.Models;
using DateTimeExtensions;
using DateTimeExtensions.Export;
using DateTimeExtensions.TimeOfDay;
using DateTimeExtensions.WorkingDays;
using Spectre.Console;
using System.Text.Json;
using WorkingWithDateOnly.Classes.Core;

using static CommonHelpersLibrary.DateTimeExtensions;

namespace WorkingWithDateOnly;
internal partial class Program
{
    static void Main(string[] args)
    {
        DisplayHolidaySchedule();

        ExtensionExamples();

        Samples();

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Displays the holiday schedule in the console.
    /// </summary>
    /// <remarks>
    /// This method generates a list of holidays, formats them, and prints them to the console.
    /// It uses Spectre.Console for styled output and includes additional date calculations.
    /// </remarks>
    private static void DisplayHolidaySchedule()
    {

        SpectreConsoleHelpers.PrintPink();

        var list = GenerateHolidayTable();

        foreach (var item in list)
        {
            AnsiConsole.MarkupLine($"[HotPink]{item.Name,-30}[/]{item.Date:MM/dd/yyyy}");
            var tempDate = item.Date.AddDays(-3);

            Console.WriteLine($" {tempDate:MM/dd/yyyy} -> {tempDate.AddWorkingDays(5):MM/dd/yyyy}");
        }

        Console.WriteLine();
    }

    /// <summary>
    /// Generates a list of holidays for the year 2026 in the United States.
    /// </summary>
    /// <param name="showTable">
    /// A boolean value indicating whether to display the holiday table in the console.
    /// If <c>true</c>, the table is displayed; otherwise, it is not.
    /// </param>
    /// <returns>
    /// A list of <see cref="HolidayItem"/> objects, each representing a holiday with its name and date.
    /// </returns>
    /// <remarks>
    /// This method retrieves holiday data using the <see cref="ExportHolidayFormatLocator"/> and processes it
    /// to create a list of holidays. Optionally, it can display the holidays in a formatted table using the
    /// Spectre.Console library. The resulting list is serialized to a JSON file named "holidays.json" if it
    /// does not already exist.
    /// </remarks>
    private static List<HolidayItem> GenerateHolidayTable(bool showTable = false)
    {

        if (showTable)
        {
            SpectreConsoleHelpers.PrintPink();
        }

        List<HolidayItem> holidays = [];

        var exporter = ExportHolidayFormatLocator.LocateByType(ExportType.OfficeHolidays);
        using var textWriter = new StringWriter();
        exporter.Export(new WorkingDayCultureInfo("en-US"), DateTime.Now.Year, textWriter);

        List<string> data = textWriter
            .ToString()
            .Split(Environment.NewLine)
            .Where(line => !string.IsNullOrWhiteSpace(line)) // last line may be empty
            .Skip(1) // Skip the header line
            .ToList();

        var table = new Table()
            .BorderColor(Color.Blue)
            .Title("[yellow]US Holidays 2026[/]")
            .AddColumn(new TableColumn("[bold yellow]Holiday[/]"))
            .AddColumn(new TableColumn("[bold yellow]Date[/]"));

        foreach (var line in data)
        {
            var parts = line.Split(',');
            if (parts.Length == 2)
            {
                DateTime date = DateTime.Parse(parts[1].Trim());
                table.AddRow(parts[0].Trim(), date.ToString("MM/dd/yyyy"));
                holidays.Add(new HolidayItem() { Name = parts[0].Trim(), Date = date });
            }
        }

        if (showTable)
        {
            AnsiConsole.Write(table);
        }

        List<HolidayItem> ordered = holidays.OrderBy(x => x.Date).ToList();

        var json = System.Text.Json.JsonSerializer.Serialize(ordered, Indented);

        var fileName = "holidays.json";

        if (!File.Exists(fileName))
        {
            File.WriteAllText(fileName, json);
        }

        return ordered;
    }

    /// <summary>
    /// Demonstrates various extension methods and utilities for working with dates, times, and business logic.
    /// </summary>
    /// <remarks>
    /// This method showcases the following examples:
    /// <list type="bullet">
    /// <item>Checking if a specific date is a working day using a culture-specific calendar.</item>
    /// <item>Determining if a date falls within daylight saving time.</item>
    /// <item>Setting a specific time for a date and verifying if it is within business hours.</item>
    /// <item>Adding a specified number of working days to the current date.</item>
    /// <item>Checking if the current time falls between two specified times.</item>
    /// </list>
    /// The results of these operations are displayed in the console using Spectre.Console for formatting.
    /// </remarks>
    public static void ExtensionExamples()
    {
        SpectreConsoleHelpers.PrintPink();

        var culture = new WorkingDayCultureInfo("en-US");

        var specificDate = new DateTime(DateTime.Now.Year, 7, 4); // Example holiday
        AnsiConsole.MarkupLine(specificDate.IsWorkingDay(culture)
            ? $"[green bold]{specificDate:yyyy-MM-dd} IS a working day.[/]"
            : $"[red bold]{specificDate:yyyy-MM-dd} is NOT a working day.[/]");

        AnsiConsole.MarkupLine(specificDate.IsDaylightSavingTime()
            ? $"[green bold]{specificDate:yyyy-MM-dd} IS in daylight saving time.[/]"
            : $"[red bold]{specificDate:yyyy-MM-dd} is NOT in daylight saving time.[/]");

        specificDate = new DateTime(2026, 4, 23);
        specificDate = specificDate.SetTime(10);
        AnsiConsole.MarkupLine(specificDate.IsWithinBusinessHours(new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0))
            ? $"[green bold]{specificDate:yyyy-MM-dd} IS within business hours.[/]"
            : $"[red bold]{specificDate:yyyy-MM-dd} is NOT within business hours.[/]");


        // Add 5 working days to a date
        DateTime futureDate = DateTime.Now.AddWorkingDays(5);
        AnsiConsole.MarkupLine($"[green bold]Add 5 working days to a {DateTime.Now:MM/dd/yyyy}:[/] " +
                               $"[yellow]{futureDate:yyyy-MM-dd}[/]");


        // Check if a time is between two other times
        bool isBetween = DateTime.Now.IsBetween(new Time(9), new Time(17));
        AnsiConsole.MarkupLine($"[green bold]Is the[/] [HotPink]{DateTime.Now:HH:mm tt}[/] [green bold]between 9 AM and 5 PM?[/] " +
                               $"[yellow]{isBetween.ToYesNo()}[/]");
    }

    /// <summary>
    /// Provides examples demonstrating the usage of various date and time extension methods 
    /// and Spectre.Console helpers.
    /// </summary>
    /// <remarks>
    /// This method showcases functionalities such as adding working days to dates, 
    /// checking if a date is a working day or holiday, calculating natural language 
    /// differences between dates, and determining if a time falls within a specific range.
    /// </remarks>
    private static void Samples()
    {

        SpectreConsoleHelpers.PrintPink();

        // Add 7 working days to a date
        DateTime futureDate = DateTime.Now.AddWorkingDays(7);
        var futureDate1 = AddWorkingDays(new DateOnly(2026, 4, 24), 7);

        // Check if a date is a working day
        bool isWorkingDay = DateTime.Now.IsWorkingDay();

        var todayDateOnly = DateOnly.FromDateTime(DateTime.Now);
        isWorkingDay = todayDateOnly.IsWorkingDay();

        // Get the difference between dates in natural language
        string dateDiff = DateTime.Now.ToNaturalText(DateTime.Now.AddDays(45));

        // Check if a time is between two other times
        bool isBetween = DateTime.Now.IsBetween(new Time(9), new Time(17));

        var friday = new DateTime(2026, 4, 24); // A friday
        var friday_plus_two_working_days = friday.AddWorkingDays(2);

        var christmasDay = new DateOnly(2026, 12, 25);
        var isHoliday = christmasDay.IsHoliday();

        var dictHolidays = new DateOnly(DateTime.Now.Year, 12, 25).AllYearHolidays();

        AnsiConsole.MarkupLine($"[HotPink bold]All year holidays for {DateTime.Now.Year}:[/]");
        foreach (var (holidayDate, value) in dictHolidays)
        {
            AnsiConsole.MarkupLine(holidayDate.Month == DateTime.Now.Month
                ? $"{holidayDate:MM/dd/yyyy} {value.Name} :check_mark:"
                : $"{holidayDate:MM/dd/yyyy} {value.Name}");
        }
    }
    public static JsonSerializerOptions Indented => new() { WriteIndented = true };
}

