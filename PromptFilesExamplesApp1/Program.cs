using PromptFilesExamplesApp1.Classes;

namespace PromptFilesExamplesApp1;
internal partial class Program
{
    static void Main(string[] args)
    {
        DataOperations.GetSettings();
        SpectreConsoleHelpers.ExitPrompt();
    }
}
