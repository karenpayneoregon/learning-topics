using Spectre.Console;
using WithStatementApp.Classes;
using WithStatementApp.Classes.Core;

namespace WithStatementApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            SpectreConsoleHelpers.PrintPink();
            //Samples.BaseExample();
            Samples.ProductsExample();
            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }
    }
}
