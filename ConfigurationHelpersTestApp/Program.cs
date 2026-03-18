using CommonHelpersLibrary;
using ConfigurationHelpersTestApp.Classes.Core;
using Spectre.Console;

namespace ConfigurationHelpersTestApp;
internal partial class Program
{
    static void Main(string[] args)
    {

        if (ConfigurationHelpers.PropertyExists("Logging", "LogLevel", "Microsoft.EntityFrameworkCore.Database.Command"))
        {
            // Handle the case where the property exists
        }
        else
        {
            // Handle the case where the property does not exist
        }


        if (ConfigurationHelpers.MainConnectionExists())
        {
            SpectreConsoleHelpers.PinkPill(Justify.Left, "Main connection exists");
        }
        else
        {
            SpectreConsoleHelpers.ErrorPill(Justify.Left, "Main connection does not exist");
        }


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
