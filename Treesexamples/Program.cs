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
            //Basics.Example1();
            //Basics.Example2();
            DirectoryTree.Display(DirectoryHelper.ProjectFolder()!);
            //WineOperations.GetWinesTree();
            //BookOperations.BooksGroupings();
            //MenuSystem.Run();
            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }

    }
}
