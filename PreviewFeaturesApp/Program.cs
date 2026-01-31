using PreviewFeaturesApp.Classes;
using PreviewFeaturesApp.Classes.Core;
using Spectre.Console;

namespace PreviewFeaturesApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        string firstName = "KAREN";
        string lastName = "SMITH";

        var fullName = $"{firstName.CapitalizeFirstLetter()} {lastName.CapitalizeFirstLetter()}";
        AnsiConsole.MarkupLine($"[bold green]{fullName}[/] using [DeepPink1]CapitalizeFirstLetter[/]");
        
        var emptyVariable = "";
        AnsiConsole.MarkupLine($"[bold green]{emptyVariable.IsEmptyField.IsEmpty()}[/] using [DeepPink1]IsEmptyField[/]");

        emptyVariable = "Value";
        AnsiConsole.MarkupLine($"[bold green]{emptyVariable.IsEmptyField.IsEmpty()}[/] using [DeepPink1]IsEmptyField[/]");

        AnsiConsole.MarkupLine($"[bold green]Mocked.GetCategories()[/][DeepPink1].Iterate[/]");
        Mocked.GetCategories().Iterate(c => $"{c.Id,-3} {c.Name}");
        Console.WriteLine();

        Console.WriteLine(1.Between(0, 2));
        Console.WriteLine(1.IsBetween(0, 2));
        Console.WriteLine(1.BetweenExclusive(0, 2));
        Console.WriteLine(1.IsBetweenExclusive(0, 2));

        SpectreConsoleHelpers.ExitPrompt();
    }
}
