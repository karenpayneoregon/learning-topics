using Spectre.Console;
using SpectreConsoleMenuApp.Classes;

namespace SpectreConsoleMenuApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            var menuItem = AnsiConsole.Prompt(MenuOperations.SelectionPrompt());
            menuItem.Action();
        }
    }
}
