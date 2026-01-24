using MenuImageSampleApp.Classes;
using Spectre.Console;
// ReSharper disable FunctionNeverReturns

namespace MenuImageSampleApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            var menuItem = AnsiConsole.Prompt(MenuOperations.SelectionPrompt());
            menuItem.Action(menuItem.Id);
        }
    }
}
