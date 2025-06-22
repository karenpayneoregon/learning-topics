using ConsoleConfigurationLibrary.Classes;
using PromptFilesExamplesApp.Classes;
using PromptFilesExamplesApp.Models;
using static PromptFilesExamplesApp.Classes.SpectreConsoleHelpers;

namespace PromptFilesExamplesApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        //GenerateAndDisplayCustomers();
        ValidateConnectionStringsOnStart();
        Console.ReadLine();
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