using DirectoryHelperSample1.Classes;
using Spectre.Console;

namespace DirectoryHelperSample1;

internal class Program
{
    static void Main(string[] args)
    {
        Console.Title = "UpperFolders Sample";
        var folders = AppDomain.CurrentDomain.BaseDirectory.UpperFolders();

        foreach (var (key, value) in folders)
        {
            Console.WriteLine($"{key}: {value}");
        }

        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold yellow]Specific Folders from the End:[/]");
        Console.WriteLine($" Project folder: {folders.FromEnd(3).Value}");
        Console.WriteLine($"Solution folder: {folders.FromEnd(4).Value}");
        Console.ReadLine();
    }
}
