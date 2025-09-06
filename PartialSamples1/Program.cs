using PartialSamples1.Classes;
using PartialSamples1.Classes.Configurations;
using Spectre.Console;

namespace PartialSamples1;
internal partial class Program
{
    static void Main(string[] args)
    {
        var clients = MockedData.RandomizeClients();

        AnsiConsole.MarkupLine("[bold yellow]Randomized Client List:[/]");
        foreach (var client in clients)
        {
            Console.WriteLine(client);
        }
        
        SpectreConsoleHelpers.ExitPrompt();
    }
}
