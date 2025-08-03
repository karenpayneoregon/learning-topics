using Ganss.Excel;
using Serilog;
using SpreadSheetLightTableSample.Classes;
using SpreadSheetLightTableSample.Models;
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

        try
        {
            await MapAndFetchCustomerData(excelFileName);
        }
        catch (Exception ex)
        {
            Log.Error(ex,"Reading Excel file");
            AnsiConsole.MarkupLine($"[red]Error while fetching data from Excel: {ex.Message}[/]");
        }

        AnsiConsole.MarkupLine("[yellow]Done[/]");
        Console.ReadLine();
    }


    /// <summary>
    /// Maps columns from an Excel file to properties of the <see cref="CustomerReportView"/> class 
    /// and fetches customer data.
    ///
    /// Mapping needs to be done as in creating the Excel file, the column names are not the same as the properties in the class.
    /// 
    /// </summary>
    /// <param name="excelFileName">The name of the Excel file containing customer data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task MapAndFetchCustomerData(string excelFileName)
    {
        Console.Clear();
        Console.Title = "Results";

        ExcelMapper excel = new();

        excel.AddMapping<CustomerReportView>("First Name", c => c.ContactFirstName);
        excel.AddMapping<CustomerReportView>("Last Name", c => c.ContactLastName);
        excel.AddMapping<CustomerReportView>("Gender", c => c.GenderType);
        excel.AddMapping<CustomerReportView>("Contact Type", c => c.ContactType);
        var customers = (await excel.FetchAsync<CustomerReportView>(excelFileName)).ToList();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Centered()
            .Title($"[{Color.Pink3}]Customers[/]")
            .AddColumn("[cyan]Id[/]")
            .AddColumn("[cyan]First name[/]")
            .AddColumn("[cyan]Last name[/]")
            .AddColumn("[cyan]Gender[/]")
            .AddColumn("[cyan]Contact Type[/]");

        foreach (var customer in customers)
        {
            table.AddRow(
                customer.Identifier.ToString(),
                customer.ContactFirstName,
                customer.ContactLastName,
                customer.GenderType.ToString(),
                customer.ContactType.ToString());
        }

        AnsiConsole.Write(table);

    }
}