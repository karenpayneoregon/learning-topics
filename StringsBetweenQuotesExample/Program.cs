using StringsBetweenQuotesExample.Classes;
using static System.Globalization.DateTimeFormatInfo;
using System.Text.RegularExpressions;
using StringsBetweenQuotesExample.Models;
using NodaTime;
using NodaTime.Text;
// ReSharper disable ConvertToLocalFunction

// ReSharper disable FormatStringProblem

namespace StringsBetweenQuotesExample;

internal partial class Program
{
    private static void Main(string[] args)
    {
        ProcessAndDisplayWeeks();

        Console.ReadLine();
        ConfigurationHelpers.ConfigurationBuilder();
        Console.WriteLine(AppConfiguration.Instance.MainConnection);

        Console.WriteLine();
        var hp1 = AppConfiguration.Instance.HelpDesk;
        var hp2 = AppConfiguration.Instance.ReadSection<HelpDesk>(nameof(HelpDesk));

        try
        {
            var counter = new HtmlTableCounter(@"C:\OED\WebApps\CF11_Jobs");
            int count = counter.CountTableTags();
            Console.WriteLine($"Total <table> tags found: {count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }


        Console.ReadLine();
    }

    private static void CalculateAndDisplayTimeDifference()
    {
        DateTime futureDate = new(2025, 3, 26, 14, 30, 0);
        Console.WriteLine(DateCalculator.CalculateTimeDifference(futureDate));

        var calendarSystem = CalendarSystem.Gregorian;
        var start = new LocalDateTime(2025, 3, 10, 15, 0, calendarSystem);
        var end = new LocalDateTime(2025, 6, 1, 5, 10, calendarSystem);

        var timeLeft = Period.Between(start, end, PeriodUnits.Days | PeriodUnits.Hours | PeriodUnits.Minutes | PeriodUnits.Seconds)
            .ToDuration();

        Console.WriteLine(timeLeft);
    }

    private static void NodaTimeGetTimeDifferenceMessage()
    {
        Console.WriteLine(NodaHelpers.GetTimeDifference(new DateTime(2025, 4, 23, 23, 59, 59)));
    }

    private static void GenerateAndDisplayMonthDetails()
    {
        IEnumerable<Month> months = CurrentInfo!.MonthNames[..^1].Select((name, index) =>
            new Month(index + 1, name));

        var indexed = months.ToList().Get();
        foreach (var item in indexed)
        {

            Console.WriteLine($"{item.Index,-4}{item.Value.Name,-12} Start: {item.StartIndex,-4} End: {item.EndIndex}");
        }
    }

    private static void ProcessAndDisplayWeeks()
    {
        var nextWeeksDates = DateTimeHelpers.NextWeeksDates();
        var days = DateTimeHelpers.GetMonthDays(DateTime.Now.Month);

        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;

        List<List<DateOnly>> weeks = DateTimeHelpers.GetWeeksInMonth(year, month);

        foreach (var (index, week) in weeks.Index())
        {
            Console.WriteLine($"Week {index + 1}: {string.Join(", ", week)}");
        }
    }

    private static void DomainSample()
    {
        WorkingDemo.Set();
        var ts = WorkingDemo.Get();
        if (ts != null)
        {
            AnsiConsole.MarkupLine($"[yellow]Regular Expressions domain Timeout[/] " +
                                   $"[white]{ts.Value.Format(false)}[/]");

            string formatted = $"{ts.Value.Days:#0:;;\\}{ts.Value.Hours:#0:;;\\}{ts.Value.Minutes:00:}{ts.Value.Seconds:00}";
            AnsiConsole.MarkupLine($"[yellow]Regular Expressions domain Timeout[/] " +
                                   $"[white]{formatted}[/]");
        }
    }

    private static void SetRegexTimeoutAndDisplay()
    {
        var timespan = Regex.InfiniteMatchTimeout;
        // set regular expression timeout from appsettings.json
        RegexOperations.SetTimeout();

        /*
         * If no timeout there is no default timeout for regular expression operations.
         */
        TimeSpan? time = RegexOperations.GetTimeout();
        string formatted = $"{time?.Days:#0:;;\\}{time?.Hours:#0:;;\\}{time?.Minutes:00:}{time?.Seconds:00}";
        AnsiConsole.MarkupLine($"[yellow]Regular Expressions domain Timeout[/] " +
                               $"[white]{time.Value.Format()}[/]");
    }

    private static void ProcessPersonDetails()
    {
        var p = new Person("Karen", "Payne", new DateOnly(1960, 12, 25));


        if (p.FirstName == "Karen")
        {
            p = p with { FirstName = "Jane" };
        }

        if (p.BirthDate == new DateOnly(1960, 12, 25))
        {
            p = p with { BirthDate = new DateOnly(1956, 1, 2) };
        }

        Console.WriteLine($"Hello {p.FirstName} {p.LastName} born {p.BirthDate:MM/dd/yyyy}");
    }

    private static void ProcessAndModifyResult()
    {
        FormattableString result = Example("Karen", "Payne", new DateOnly(1960, 12, 25));
        if (result.FirstName() == "Karen")
        {
            result.UpdateFirstName("Jane");
        }


        if (result.BirthDate() == new DateOnly(1960, 12, 25))
        {
            result.UpdateBirth(new DateOnly(1956, 1, 2));
        }

        Console.WriteLine(result);
    }

    private static FormattableString Example(string firstName, string lastName, DateOnly birthDate) =>
        $"Hello {firstName} {lastName} born {birthDate}";

    private static void ExtractAndPrintQuotedStrings()
    {
        const string text = "Splits a \"PascalCase\" or \"camelCase\" string into " +
                            "separate \"words\" by inserting \"spaces\" before \"uppercase letters\".";

        var parts = text.StringsBetweenQuotes2(false);
        foreach (var item in parts)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }
}

public static class Extensions
{
    public static string FirstName(this FormattableString sender)
        => (string)sender.GetArguments()[0];

    public static string LastName(this FormattableString sender)
        => (string)sender.GetArguments()[1];


    public static DateOnly BirthDate(this FormattableString sender)
        => ((DateOnly)sender.GetArguments()[2]);

    public static void UpdateFirstName(this FormattableString sender, string value)
    {
        sender.GetArguments()[0] = value;
    }
    public static void UpdateLastName(this FormattableString sender, string value)
    {
        sender.GetArguments()[1] = value;
    }
    public static void UpdateBirth(this FormattableString sender, DateOnly value)
    {
        sender.GetArguments()[2] = value;
    }
}

public static class RangeHelpers
{

    public static List<Container<T>> Get<T>(this List<T> sender) =>
        sender.Select((element, index) => new Container<T>
        {
            Value = element,
            StartIndex = new(index),
            EndIndex = new(sender.Count - index - 1, true),
            Index = index + 1
        }).ToList();

    public static List<Container<T>> Get<T>(this T[] sender) =>
        sender.Select((element, index) => new Container<T>
        {
            Value = element,
            StartIndex = new(index),
            EndIndex = new(sender.Length - index - 1, true),
            Index = index + 1
        }).ToList();
}


public class DateCalculator
{
    /// <summary>
    /// Calculates the time difference between the current date and a specified future date.
    /// </summary>
    /// <param name="futureDate">The future date for which the time difference is to be calculated. 
    /// This date must be later than the current date and time.</param>
    /// <returns>A string representing the time difference in terms of months, days, hours, and minutes. 
    /// If the provided date is not in the future, a message indicating this is returned.</returns>
    public static string CalculateTimeDifference(DateTime futureDate)
    {
        if (futureDate <= DateTime.Now)
        {
            return "The provided date must be in the future.";
        }

        DateTime now = DateTime.Now;

        // Calculate total months difference
        int months = ((futureDate.Year - now.Year) * 12) + futureDate.Month - now.Month;

        if (futureDate.Day < now.Day)
        {
            months--; // Adjust if the future day is earlier than the current day
        }

        // Calculate remaining days, hours, and minutes
        TimeSpan timeSpan = futureDate - now.AddMonths(months);
        int days = timeSpan.Days;
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;

        // Build the result dynamically
        return $"{(months > 0 ? $"{months} months, " : "")}{days} days, {hours} hours, {minutes} minutes.";
    }
}

