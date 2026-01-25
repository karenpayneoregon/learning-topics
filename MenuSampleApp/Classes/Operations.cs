using ConsoleConfigurationLibrary.Classes;
using MenuSampleApp.Classes.Core;
using MenuSampleApp.Models;
using Microsoft.Data.SqlClient;
using Serilog;
using Spectre.Console;
using static MenuSampleApp.Classes.Core.SpectreConsoleHelpers;

namespace MenuSampleApp.Classes;
/// <summary>
/// Provides a collection of static methods for performing various operations 
/// related to the NorthWind database, including displaying table counts, 
/// viewing categories, grouping customers by country, and exiting the application.
/// </summary>
/// <remarks>
/// This class contains utility methods that interact with the database and 
/// provide feedback to the user. It includes functionality for checking database 
/// existence, retrieving and displaying data, and managing user interactions.
///
/// --- Stored procedures can be an option. ---
/// 
/// </remarks>
/// <example>
/// The <see cref="MenuSampleApp.Classes.Operations"/> class can be used in conjunction 
/// with menu operations to execute specific tasks. For example:
/// <code>
/// var menu = MenuOperations.MainSelectionPrompt();
/// menu.Show();
/// </code>
/// </example>
internal class Operations
{

    /// <summary>
    /// Represents the name of the database extracted from the application's main connection string.
    /// </summary>
    /// <remarks>
    /// This field is initialized using the connection string provided by the application's configuration.
    /// It is used in various operations to verify the existence of the database and to perform database-related tasks.
    /// </remarks>
    private static readonly string DatabaseName = DataOperations.InitialCatalogFromConnectionString(AppConnections.Instance.MainConnection);
    
    /// <summary>
    /// Checks whether the database specified in the application's main connection string exists.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the database exists; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method utilizes the database name extracted from the application's main connection string
    /// and verifies its existence by querying the master database.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue connecting to the database or executing the SQL command.
    /// </exception>
    private static bool DatabaseExists() => DataOperations.ExpressDatabaseExists(DatabaseName);

    /// <summary>
    /// Displays the count of tables and their respective row counts in the NorthWind database.
    /// </summary>
    /// <remarks>
    /// This method retrieves the database name from the application's main connection string.
    /// It checks if the database exists and, if so, fetches and displays the table names along with their row counts.
    /// If the database does not exist, it logs an informational message and notifies the user.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown if there is an issue connecting to the database or executing the SQL commands.
    /// </exception>
    public static void NorthWindTableCount()
    {

        PrintCyan();


        if (DatabaseExists())
        {
            var tables = DataOperations.TablesCount(AppConnections.Instance.MainConnection);
            foreach (var table in tables)
            {
                Console.WriteLine($"{table.TableName,-25}{table.RowCount}");
            }
        }
        else
        {
            Console.WriteLine($"Database {DatabaseName} does not exist.");
            Log.Information("Database {databaseName} does not exist.", DatabaseName);
        }


        Continue();

    }

    /// <summary>
    /// Displays the list of categories from the database, including their IDs and names.
    /// </summary>
    /// <remarks>
    /// This method verifies the existence of the database before attempting to retrieve the categories.
    /// If the database exists, it fetches and displays the list of categories with their IDs and names.
    /// If the database does not exist, it logs an informational message and notifies the user.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue connecting to the database or executing SQL commands.
    /// </exception>
    public static void ViewCategories()
    {
        PrintCyan();

        if (DatabaseExists())
        {
            var categories = DataOperations.GetCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryId}: {category.CategoryName}");
            }

        }
        else
        {
            Console.WriteLine($"Database {DatabaseName} does not exist.");
            Log.Information("Database {databaseName} does not exist.", DatabaseName);
        }


        Continue();
    }

    /// <summary>
    /// Retrieves and displays a list of customers grouped by their respective countries.
    /// </summary>
    /// <remarks>
    /// This method fetches customer data grouped by country using <see cref="DataOperations.GetCustomersGroupedByCountry"/>.
    /// The results are displayed in a formatted manner using Spectre.Console utilities.
    /// </remarks>
    /// <exception cref="SqlException">
    /// Thrown when there is an issue with database connectivity or query execution.
    /// </exception>
    public static void GetCustomersGroupedByCountry()
    {

        PrintCyan();
        
        IEnumerable<IGrouping<string, CustomerCountry>> groups = DataOperations.GetCustomersGroupedByCountry();


        foreach (var group in groups)
        {
            AnsiConsole.MarkupLine($"[cyan]Country:[/] [DeepPink1]{group.Key}[/]");

            foreach (var customer in group.OrderBy(x => x.CompanyName))
            {
                AnsiConsole.MarkupLine($"  {customer.CompanyName}");
            }
        }
        
        Continue();
        
    }
    /// <summary>
    /// Exits the application immediately by terminating the current process.
    /// </summary>
    /// <remarks>
    /// This method calls <see cref="Environment.Exit(int)"/> with an exit code of 0,
    /// which indicates a successful termination of the application.
    /// </remarks>
    public static void ExitApplication()
    {
        Environment.Exit(0);
    }
}
