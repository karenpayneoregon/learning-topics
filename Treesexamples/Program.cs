using Spectre.Console;
using TreesExamples.Classes;
using TreesExamples.Classes.Core;
using TreesExamples.Models;

namespace TreesExamples
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            WineOperations.GetWinesTree();
            BookOperations.BooksGroupings();

            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }

    }
}
