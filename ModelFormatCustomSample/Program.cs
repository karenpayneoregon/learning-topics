using ModelFormatCustomSample.Classes;
using ModelFormatCustomSample.Models;
using Spectre.Console;

namespace ModelFormatCustomSample;
internal partial class Program
{
    static void Main(string[] args)
    {
        Customer customer = new(1, "Karen", "Payne", new DateOnly(1956, 9, 24));

        AnsiConsole.MarkupLine($"[cyan]Age[/]: {customer:Age}");
        AnsiConsole.MarkupLine($"[cyan]IFL[/]: {customer:IFL}");
        AnsiConsole.MarkupLine($"[cyan] FL[/]: {customer:FL}");
        AnsiConsole.MarkupLine($"[cyan]  B[/]: {customer:B}");
        AnsiConsole.MarkupLine($"[cyan]  I[/]: {customer:I}");

        Console.WriteLine();

        var (firstName, lastName, birthDate) = customer;
        AnsiConsole.MarkupLine($"[cyan]Deconstruct[/]: {firstName} {lastName}, born on {birthDate}");
        SpectreConsoleHelpers.ExitPrompt();
    }
}
