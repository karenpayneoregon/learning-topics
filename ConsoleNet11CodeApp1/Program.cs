using ConsoleNet11CodeApp1.Classes.Core;
using Spectre.Console;

namespace ConsoleNet11CodeApp1;

internal partial class Program
{
    static void Main(string[] args)
    {

        HashSet<string> tags = [with(StringComparer.OrdinalIgnoreCase), "one", "two", "three"];

        foreach (var (index, tag) in tags.Index())
        {
            AnsiConsole.MarkupLine($"[green]{index}[/]: [cyan]{tag}[/]");
        }

        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}