using SpreadSheetLightTableSample.Classes;
using SpreadSheetLightTableSampleLibrary.Classes;
using SpreadSheetLightTableSampleLibrary.Classes.LanguageExtensions;

namespace SpreadSheetLightTableSample;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        AnsiConsole.MarkupLine("[yellow]Reading data[/]");
        var (data, isSuccess) = await DataOperations.GetCustomerReportData();
        if (!isSuccess)
        {
            AnsiConsole.MarkupLine("[red]Failed to retrieve data.[/]");
            return;
        }

        var table = data.ToDataTable();

        var excelFileName = "Customers.xlsx";
        AnsiConsole.MarkupLine("[cyan]Creating Excel file[/]");
        if (!await FileHelpers.CanReadFile(excelFileName))
        {
            AnsiConsole.MarkupLine("[red]Excel file is currently open.[/] [cyan]Please close it followed by pressing ENTER.[/]");
            Console.ReadLine();
        }

        if (!ExcelOperations.CreateCustomerViewReport(table, excelFileName))
        {
            AnsiConsole.MarkupLine("[red]Failed to create Excel report.[/]");
            return;
        }

        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();
    }
}