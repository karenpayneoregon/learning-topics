using GlobbingApp1.Classes;
using GlobbingApp1.Classes.Core;
using Spectre.Console;
       
namespace GlobbingApp1;
internal partial class Program
{
    private static async Task Main(string[] args)
    {

        var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var list = await Globbing.GetWordDocumentsTask(folder);

        await GlobbingSamples.ProcessOneDriveDuplicates();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
