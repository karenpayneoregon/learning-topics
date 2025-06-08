using PrintMembersSamples.Classes;

namespace PrintMembersSamples;

internal partial class Program
{
    static void Main(string[] args)
    {
        var people = MockedData.SamplePeople();
        AnsiConsole.MarkupLine("[cyan]First     Last      SSN          Birthdate   Phone numbers[/]");
        foreach (var person in people)
        {
            AnsiConsole.MarkupLine(person.Colorize());
        }

        SpectreConsoleHelpers.ExitPrompt();
    }
}