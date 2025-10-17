using Spectre.Console;
using VsWhereDataApp.Models;

namespace VsWhereDataApp.Classes;
internal class MainOperation
{
    public static void Display()
    {
        string outputPath = Path.Combine(AppContext.BaseDirectory, "vs.json");
        FileOperations.GenerateDataJson(outputPath);

        Installation? data = FileOperations.ReadDataJson(outputPath).LastOrDefault();

        Console.WriteLine();
        AnsiConsole.MarkupLine($"[green3_1]        Edition[/] {data?.DisplayName}");
        AnsiConsole.MarkupLine($"[green3_1]        Version[/] {data?.InstallationVersion}");
        AnsiConsole.MarkupLine($"[green3_1]Display Version[/] {data?.Catalog.ProductDisplayVersion}");
        AnsiConsole.MarkupLine($"[green3_1]           Path[/] {data?.InstallationPath}");
        AnsiConsole.MarkupLine($"[green3_1]   Install Date[/] {data?.InstallDate} ");
        AnsiConsole.MarkupLine($"[green3_1]    Update Date[/] {data?.UpdateDate} ");
        AnsiConsole.MarkupLine($"[green3_1]  Release Notes[/] {data?.ReleaseNotes} ");
        Console.WriteLine();
    }
}
