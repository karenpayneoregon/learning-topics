using GlobbingApp2.Classes;
using GlobbingApp2.Classes.Core;
using Spectre.Console;

namespace GlobbingApp2
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {

            SpectreConsoleHelpers.PrintPink();

            List<FileMatchItem> list = await Globbing.GetWordDocumentsTask();

            if (list.Count >0)
            {
                AnsiConsole.MarkupLine("[green]Word documents found:[/]"                                                                    );
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                SpectreConsoleHelpers.WarningPill(Justify.Left, "No Word documents found.");
            }   

            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }
    }
}
