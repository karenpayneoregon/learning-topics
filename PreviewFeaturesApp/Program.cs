using PreviewFeaturesApp.Classes;
using PreviewFeaturesApp.Classes.Core;
using PreviewFeaturesApp.Classes.Extensions;
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

        AnsiConsole.MarkupLine($"[bold green]using [DeepPink1]Between[/][/]");
        Console.WriteLine();

        AnsiConsole.MarkupLine($"[bold cyan]1.Between(0, 2)[/] {1.Between(0, 2).ToYesNo()}");
        AnsiConsole.MarkupLine($"[bold cyan]14.Between(0, 2)[/] {14.Between(0, 2).ToYesNo()}");

        foreach (var (index, item) in MockedTimeOnlyItem.List.Index())
        {
            AnsiConsole.MarkupLine($"{index,-3}Is " +
                                   $"{PrintValue(item.Value)} between " +
                                   $"{PrintValue(item.Start)} and {PrintValue(item.End)}? " +
                                   $"{PrintYes(item.Value.Between(item.Start, item.End).ToYesNo())}");
        }
        
        Console.WriteLine();
        
        SpectreConsoleHelpers.ExitPrompt();
    }


}
