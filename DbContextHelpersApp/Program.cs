using DbContextHelpersApp.Classes.Core;
using DbContextHelpersApp.Models;
using DbContextHelpersApp.Classes;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace DbContextHelpersApp;

internal partial class Program
{
    static void Main(string[] args)
    {

        SpectreConsoleHelpers.PrintPink();

        using var context = new Context();
        if (context.DatabaseExists())
        {
            if (context.TablesExist("Customers", "Countries"))
            {
                var customer = context.Customers
                    .Include(c => c.CountryNavigation)
                    .FirstOrDefault();

                AnsiConsole.MarkupLine($"[green bold]Customer: {customer?.CompanyName} Country: {customer?.CountryNavigation?.Name}[/]");
            }
            else
            {
                Console.WriteLine("Customers Countries are required.");
            }
        }
        else
        {
            Console.WriteLine("Database does not exist.");
        }


        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}