using Net11CodeApp.Classes.Core;
using Spectre.Console;

namespace Net11CodeApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}