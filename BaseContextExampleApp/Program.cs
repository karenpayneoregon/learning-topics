using BaseContextExampleApp.Classes;
using BaseContextExampleApp.Data;
using Spectre.Console;

namespace BaseContextExampleApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        using var context = new Context();
        var birthDays = context.BirthDays.ToList();

        AnsiConsole.MarkupLine("[bold u]Id  First          Last           Birth          Age[/]");

        foreach (var item in birthDays)
        {
            Console.WriteLine($"{item.Id, -4}{item.FirstName, -15}{item.LastName, -15}{item.BirthDate, -15:MM/dd/yyyy}{item.YearsOld}");
        }
        SpectreConsoleHelpers.ExitPrompt();
    }
}
