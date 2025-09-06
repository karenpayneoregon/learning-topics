using PartialSamples1.Classes;
using PartialSamples1.Classes.Configurations;
using PartialSamples1.Classes.Extensions;
using Spectre.Console;

namespace PartialSamples1;
internal partial class Program
{
    private static void Main(string[] args)
    {
        var clients = MockedData.RandomizeClients();

        AnsiConsole.MarkupLine("[bold yellow]Randomized Client List:[/]");
        foreach (var client in clients)
        {
            Console.WriteLine(client);
        }
        
        var people = MockedData.RandomizePeople();

        AnsiConsole.MarkupLine("[bold yellow]Randomized People List:[/]");
        foreach (var person in people)
        {
            Console.WriteLine(person);
        }


        AnsiConsole.MarkupLine("[bold yellow]SSN Masked:[/]");
        var ssnList = MockedData.GeneratedSocialSecurityNumbers();
        
        foreach (var ssn in ssnList)
        {
            Console.WriteLine($"{ssn, -15}{ssn.MaskSsn()}");
        }
        SpectreConsoleHelpers.ExitPrompt();
    }
}
