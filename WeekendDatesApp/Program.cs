using CommonHelpersLibrary;
using Spectre.Console;
using WeekendDatesApp.Classes;

namespace WeekendDatesApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        {
            AnsiConsole.MarkupLine("[cyan]Previous Weekend Dates[/]");
            var (saturday, sunday) = DateTime.Now.GetPreviousWeekendDates();
            Console.WriteLine($"{saturday} - {sunday}\n");
        }
        
        {
            AnsiConsole.MarkupLine("[cyan]Weekend Dates[/]");
            var (saturday, sunday) = DateTime.Now.GetWeekendDates();
            Console.WriteLine($"{saturday} - {sunday}\n");
        }

        {
            AnsiConsole.MarkupLine("[cyan]Next Weekend Dates[/]");
            var (saturday, sunday) = DateTime.Now.GetNextWeekendDates();
            Console.WriteLine($"{saturday} - {sunday}");
        }
        
        SpectreConsoleHelpers.ExitPrompt();
    }
}
