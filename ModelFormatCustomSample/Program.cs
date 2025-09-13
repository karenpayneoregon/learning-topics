using ModelFormatCustomSample.Classes;
using ModelFormatCustomSample.Models;
using Spectre.Console;

namespace ModelFormatCustomSample;
internal partial class Program
{
    static void Main(string[] args)
    {
        Customer customer = new(1, "Karen", "Payne", new DateOnly(1956, 9, 24));

        AnsiConsole.MarkupLine($"[cyan]A[/]: {customer:A}");
        AnsiConsole.MarkupLine($"[cyan]F[/]: {customer:F}");
        AnsiConsole.MarkupLine($"[cyan]N[/]: {customer:N}");
        AnsiConsole.MarkupLine($"[cyan]B[/]: {customer:B}");
        AnsiConsole.MarkupLine($"[cyan]I[/]: {customer:I}");
        AnsiConsole.MarkupLine($"[cyan]C[/]: {customer}");
        AnsiConsole.MarkupLine($"[cyan]T[/]: {customer.ToString()}");

        Console.WriteLine();

        var (firstName, lastName, birthDate) = customer;
        AnsiConsole.MarkupLine($"[cyan]Deconstruct[/]: {firstName} {lastName}, born on {birthDate}");
        SpectreConsoleHelpers.ExitPrompt();
    }
}
