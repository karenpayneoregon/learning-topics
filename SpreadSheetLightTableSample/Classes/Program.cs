using System.Runtime.CompilerServices;
using SpreadSheetLightTableSample.Classes;

// ReSharper disable once CheckNamespace
namespace SpreadSheetLightTableSample;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "SpreadSheetLight tables";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        SetupLogging.ToFile();

    }
}
