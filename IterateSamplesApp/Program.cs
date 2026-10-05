using CommonHelpersLibrary;
using IterateSamplesApp.Classes;
using Spectre.Console;
using System.Runtime.InteropServices.Marshalling;
using static IterateSamplesApp.Classes.GenericIterators;

namespace IterateSamplesApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        Iterate(1, 2, 3, 4, 5);
        Console.WriteLine();
        
        Iterate("Sam", "Anne", "Mary", "Dan", "Kim");
        Console.WriteLine();
        
        Mocked.GetCategories().Iterate(c => $"{c.Id,-3} {c.Name}");
        Console.WriteLine();
        
        Mocked.GetCategories().Iterate(category => category.ToString());

        Console.WriteLine();
        
        string[] names =
        [
            "Karen",
            "mARY",
            "John"
        ];

        names.Iterate(name => name.CapitalizeFirstLetter(), 
            (index, value) => Console.WriteLine($"{index,-5}{value}"));

        Console.WriteLine();
        
        names.Iterate(name => name.CapitalizeFirstLetter(), 
            (index, value) => AnsiConsole.MarkupLine($"[cyan]{index,-5}{value}[/]"));

        SpectreConsoleHelpers.ExitPrompt();
    }
}
