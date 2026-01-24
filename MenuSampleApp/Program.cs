using MenuSampleApp.Classes;
using Spectre.Console;
// ReSharper disable FunctionNeverReturns ;;

namespace MenuSampleApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        while (true)
        {
            Console.Clear();
            var menuItem = AnsiConsole.Prompt(MenuOperations.MainSelectionPrompt());
            menuItem.Action();
        }
    }
}
