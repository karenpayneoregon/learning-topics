using ConsoleNet11CodeApp1.Classes.Core;
using Spectre.Console;

namespace ConsoleNet11CodeApp1;

internal partial class Program
{
    static void Main(string[] args)
    {

        DisplayWithIndex();
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }

    /// <summary>
    /// Displays a list of tags along with their corresponding indices in the console.
    /// </summary>
    /// <remarks>
    /// The method uses a <see cref="HashSet{T}"/> to store tags with case-insensitive comparison
    /// and iterates through the tags using their indices. The output is formatted with colors
    /// using the Spectre.Console library.
    /// </remarks>
    private static void DisplayWithIndex()
    {
        HashSet<string> names = [with(StringComparer.OrdinalIgnoreCase), "Alice", "ALICE", "Bob"];
        HashSet<string> tags = [with(StringComparer.OrdinalIgnoreCase), "one", "two", "three"];

        foreach (var (index, tag) in tags.Index())
        {
            AnsiConsole.MarkupLine($"[green]{index}[/]: [cyan]{tag}[/]");
        }
    }
}