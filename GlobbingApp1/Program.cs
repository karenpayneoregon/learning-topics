using GlobbingApp1.Classes;
using GlobbingApp1.Classes.Core;
using Spectre.Console;

namespace GlobbingApp1;
internal partial class Program
{
    private static async Task Main(string[] args)
    {

        await GlobbingSamples.ProcessOneDriveDuplicates();
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
