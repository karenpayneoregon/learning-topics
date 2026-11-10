using ConsoleNet11CodeApp1.Classes.Core;
using Spectre.Console;

namespace ConsoleNet11CodeApp1
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        }
    }
}