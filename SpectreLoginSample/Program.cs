using Spectre.Console;
using SpectreLoginSample.Classes;
using SpectreLoginSample.Classes.Core;

namespace SpectreLoginSample;
internal partial class Program
{
    static void Main(string[] args)
    {
        if (Prompts.TryLogin())
        {
            Console.Clear();
            SpectreConsoleHelpers.PinkPill(Justify.Left, "Welcome");
            SpectreConsoleHelpers.ExitPrompt(Justify.Left);
        }

    }
}
