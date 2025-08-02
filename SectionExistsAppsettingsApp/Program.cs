using SectionExistsAppsettingsApp.Classes;
using static SectionExistsAppsettingsApp.Classes.Utilities;

// here we must indicate it's not from  Spectre.Console
using Layout = SectionExistsAppsettingsApp.Classes.Layout;

namespace SectionExistsAppsettingsApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        AnsiConsole.MarkupLine(PropertyExists(nameof(ConnectionStrings), nameof(ConnectionStrings.MainConnection))
            ? "[green]ConnectionStrings:MainConnection exists[/]"
            : "[red]ConnectionStrings:MainConnection does not exist[/]");

        Console.WriteLine();

        AnsiConsole.MarkupLine(PropertyExists(nameof(ConnectionStrings), nameof(ConnectionStrings.SecondaryConnection))
            ? "[green]ConnectionStrings:SecondaryConnection exists[/]"
            : "[red]ConnectionStrings:SecondaryConnection does not exist[/]");

        Console.WriteLine();

        AnsiConsole.MarkupLine(AllPropertiesExist(nameof(Layout), 
            nameof(Layout.Header), nameof(Layout.Footer), nameof(Layout.Title))
            ? "[green]Layout validates[/]"
            : "[red]Layout does not validate[/]");

        Console.WriteLine();

        AnsiConsole.MarkupLine("[yellow]Continue[/]");
        Console.ReadLine();
    }
}