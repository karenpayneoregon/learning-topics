using ConsoleConfigurationLibrary.Classes;
using PromptFilesExamplesApp.Classes;
using System.Reflection;
using System.Runtime.CompilerServices;
using PromptFilesExamplesApp.Models;
using static PromptFilesExamplesApp.Classes.SpectreConsoleHelpers;

// ReSharper disable once CheckNamespace
namespace PromptFilesExamplesApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
    private static void GenerateAndDisplayCustomers()
    {

        PrintCyan();

        var customers = BogusCustomer.GenerateCustomers(20);
        var table = CreateTable();
        foreach (var customer in customers)
        {
            table.AddRow(customer.FirstName, customer.LastName,
                customer.Gender == Gender.Female
                    ? $"[deepskyblue3]{customer.Gender}[/]"
                    : customer.Gender.ToString()!, customer.BirthDay.ToString("MM/dd/yyyy"), customer.Email);
        }

        AnsiConsole.Write(table);
    }
    private static void ValidateConnectionStringsOnStart()
    {

        PrintCyan();
        // Validate ConnectionStrings properties on start
        var (valid, errors) = ApplicationValidation.ValidateOnStartReporter<ConnectionStrings>(nameof(ConnectionStrings),
            cs => cs.MainConnection,
            cs => cs.SecondaryConnection
        );
        if (!valid)
        {
            AnsiConsole.MarkupLine("[red]Validation failed:[/]");
            foreach (var error in errors)
            {
                Console.WriteLine($"   {error}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"[{Color.Pink1}]Validation succeeded.[/]");
        }

        Console.WriteLine("Done");
    }
}
