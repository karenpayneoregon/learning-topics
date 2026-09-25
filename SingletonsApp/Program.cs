using SingletonsApp.Classes.Core;
using SingletonsApp.Singletons;
using Spectre.Console;

namespace SingletonsApp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {

            SpectreConsoleHelpers.PrintPink();

            var test = EnvironmentSettings.Instance.DatabaseConnectionString;

            SpectreConsoleHelpers.ExitPrompt(Justify.Left);

        }
    }
}
