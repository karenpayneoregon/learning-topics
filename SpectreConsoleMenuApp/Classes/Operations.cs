using Spectre.Console;
using SpectreConsoleMenuApp.Classes.Utilities;
using SpectreConsoleMenuApp.Models;
using static SpectreConsoleMenuApp.Classes.Core.SpectreConsoleHelpers;
using Customer = ModelFormatLibrary.Models.Customer;

namespace SpectreConsoleMenuApp.Classes;
internal class Operations
{
    /// <summary>
    /// Demonstrates custom formatting and deconstruction of a <see cref="Customer"/> object.
    /// </summary>
    /// <remarks>
    /// This method showcases the usage of custom format strings defined in the <see cref="Customer"/> class.
    /// It also demonstrates deconstruction of the <see cref="Customer"/> object into its individual properties.
    /// The output is displayed using Spectre.Console markup for enhanced console formatting.
    /// </remarks>
    /// <example>
    /// Example output:
    /// <code>
    /// Age: 67
    /// IFL: 1 Karen Payne
    /// FL: Karen Payne
    /// B: 1956-09-24
    /// I: 1
    /// Deconstruct: Karen Payne, born on 1956-09-24
    /// </code>
    /// </example>
    public static void ModelFormatCustomSample()
    {

        PrintCyan();
        
        Customer customer = new(1, "Karen", "Payne", new DateOnly(1956, 9, 24));

        AnsiConsole.MarkupLine($"[cyan]Age[/]: {customer:Age}");
        AnsiConsole.MarkupLine($"[cyan]IFL[/]: {customer:IFL}");
        AnsiConsole.MarkupLine($"[cyan] FL[/]: {customer:FL}");
        AnsiConsole.MarkupLine($"[cyan]  B[/]: {customer:B}");
        AnsiConsole.MarkupLine($"[cyan]  I[/]: {customer:I}");

        Console.WriteLine();

        var (firstName, lastName, birthDate) = customer;
        AnsiConsole.MarkupLine($"[cyan]Deconstruct[/]: {firstName} {lastName}, born on {birthDate}");

        Continue();
    }

    /// <summary>
    /// Demonstrates the process of generating and updating the next PIN value for a user.
    /// </summary>
    /// <remarks>
    /// This method initializes a <see cref="UserLogin"/> object, retrieves the current PIN using 
    /// <see cref="NextValueService.ReadPin"/>, and calculates the next PIN using 
    /// <see cref="Helpers.NextValue(string, int)"/>. The updated PIN is displayed in the console 
    /// and saved back using <see cref="NextValueService.UpdatePin(string)"/>.
    /// </remarks>
    /// <example>
    /// Example output:
    /// <code>
    /// Current pin: A1B2C23
    /// Next pin: A1B2C24
    /// User pin: A1B2C24
    /// </code>
    /// </example>
    public static void NextValueSample()
    {
        PrintCyan();

        var nextValueService = new NextValueService(Helpers.GetConfiguration());

        UserLogin user = new()
        {
            Id = 1,
            EmailAddress = "karen.payne@example.com",
            Pin = nextValueService.ReadPin()
        };

        AnsiConsole.MarkupLine($"[cyan]Current pin[/]: {user.Pin}");
        
        string nextPin = Helpers.NextValue(user.Pin!);
        
        AnsiConsole.MarkupLine($"[yellow]Next pin[/]: {nextPin}");
        
        user.Pin = nextPin;
        AnsiConsole.MarkupLine($"[cyan]User pin[/]: {user.Pin}");

        nextValueService.UpdatePin(user.Pin);
        
        Continue();
        
    }

    public static void ExitApplication()
    {
        Environment.Exit(0);
    }
}
