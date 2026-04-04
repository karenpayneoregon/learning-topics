using DeconstructDictionaryApp.Classes.Core;
using Spectre.Console;

namespace DeconstructDictionaryApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        var dictionary = Dictionary();
        
        foreach (var item in dictionary)
        {
            Console.WriteLine($"{item.Key,-4}{item.Value.Day,-4}{item.Value.Month,-4}{item.Value.Year}");
            Console.WriteLine($"{item.Key,-4}{item.Value:MM/dd/yyyy}");

        }

        Console.WriteLine();
        
        foreach (var (cellAddress, (day, month, year)) in dictionary)
        {
            Console.WriteLine($"{cellAddress,-4}{day,-4}{month,-4}{year}");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    private static Dictionary<string, DateOnly> Dictionary()
        => new()
        {
            { "H4", new(2017, 1, 1) }, { "H5", new(2017, 1, 2) },
            { "H6", new(2017, 1, 3) }, { "H7", new(2017, 1, 4) }
        };
}

