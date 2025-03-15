using System.ComponentModel.DataAnnotations;
using StringsBetweenQuotesExample.Classes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;

namespace StringsBetweenQuotesExample;

public record Person(string FirstName, string LastName, DateOnly BirthDate);
internal partial class Program
{
    private static void Main(string[] args)
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
        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();
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

