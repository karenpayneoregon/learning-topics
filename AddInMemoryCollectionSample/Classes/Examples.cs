using AddInMemoryCollectionSample.Models;
using Microsoft.Extensions.Configuration;

using Layout = AddInMemoryCollectionSample.Models.Layout; // resolves ambiguity with Spectre.Console Layout

namespace AddInMemoryCollectionSample.Classes;

internal class Examples
{

    /// <summary>
    /// Demonstrates the use of an in-memory collection to configure and retrieve application settings.
    /// </summary>
    /// <remarks>
    /// This method initializes a configuration using an in-memory collection of key-value pairs. 
    /// It retrieves and displays specific configuration values, such as connection strings and layout settings, 
    /// using the Spectre.Console library for formatted console output.
    /// </remarks>
    public static void DryRun()
    {

        SpectreConsoleHelpers.Print();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:MainConnection",
                    "Server=(localdb)\\MSSQLLocalDB;Database=NorthWind2024;Trusted_Connection=True"
                },
                { "Layout:Header", "Visible" },
                { "Layout:Title", "Some title" },
                { "Layout:Footer", "Hidden" }
            }).Build();

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {configuration["ConnectionStrings:MainConnection"]}");

        Console.WriteLine();

        foreach (var kvp in configuration.GetSection("Layout").AsEnumerable())
        {
            AnsiConsole.MarkupLine($"[cyan]Key:[/] {kvp.Key}, [cyan]Value:[/] {kvp.Value}");
        }

        Console.WriteLine();

    }

    /// <summary>
    /// Demonstrates the use of an in-memory collection to configure and retrieve application settings.
    /// </summary>
    /// <remarks>
    /// This method initializes a configuration using an in-memory collection of key-value pairs. 
    /// It retrieves and displays specific configuration values, such as connection strings and layout settings, 
    /// using the Spectre.Console library for formatted console output.
    /// </remarks>
    public static void DryRun2()
    {

        SpectreConsoleHelpers.Print();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:MainConnection",
                    "Server=(localdb)\\MSSQLLocalDB;Database=NorthWind2024;Trusted_Connection=True"
                },
                { "Layout:Header", "Visible" },
                { "Layout:Title", "Some title" },
                { "Layout:Footer", "Hidden" }
            }).Build();

        var mainConnection = configuration["ConnectionStrings:MainConnection"];
        var headerLayout = configuration["Layout:Header"];
        var titleLayout = configuration["Layout:Title"];
        var footerLayout = configuration["Layout:Footer"];


        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {mainConnection}");

        AnsiConsole.MarkupLine($"[cyan]Header:[/] {headerLayout}");
        AnsiConsole.MarkupLine($"[cyan]Title:[/] {titleLayout}");
        AnsiConsole.MarkupLine($"[cyan]Footer:[/] {footerLayout}");

        Console.WriteLine();

    }

    /// <summary>
    /// Demonstrates the usage of in-memory configuration by creating a configuration object,
    /// mapping it to strongly-typed models, and displaying the configuration values.
    /// </summary>
    /// <remarks>
    /// This method uses the <see cref="ConfigurationBuilder"/> 
    /// to build an in-memory configuration source. The configuration values are then deserialized 
    /// into <see cref="ConnectionStrings"/> and 
    /// <see cref="Layout"/> models for structured access.
    /// </remarks>
    public static void DryRun3()
    {

        SpectreConsoleHelpers.Print();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {
                    "ConnectionStrings:MainConnection",
                    "Server=(localdb)\\MSSQLLocalDB;Database=NorthWind2024;Trusted_Connection=True"
                },
                { "Layout:Header", "Visible" },
                { "Layout:Title", "Some title" },
                { "Layout:Footer", "Hidden" }
            }).Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        var layout = configuration.GetSection(nameof(Layout)).Get<Layout>();

        var mainConnection = connectionStrings.MainConnection;
        var headerLayout = layout.Header;
        var titleLayout = layout.Title;
        var footerLayout = layout.Footer;

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {mainConnection}");
        AnsiConsole.MarkupLine($"[cyan]Header:[/] {headerLayout}");
        AnsiConsole.MarkupLine($"[cyan]Title:[/] {titleLayout}");
        AnsiConsole.MarkupLine($"[cyan]Footer:[/] {footerLayout}");

        Console.WriteLine();

    }

    /// <summary>
    /// Reads configuration settings from the "appsettings.json" file and displays 
    /// the connection string and layout information in the console.
    /// </summary>
    /// <remarks>
    /// This method retrieves sections named <see cref="ConnectionStrings"/> and 
    /// <see cref="Layout"/> from the configuration file. It then extracts and 
    /// prints the main connection string, header layout, title layout, and footer layout.
    /// </remarks>
    public static void ReadFromAppsettingsFile()
    {

        SpectreConsoleHelpers.Print();


        var configuration = ConfigurationHelpers.configurationBuilder();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        var layout = configuration.GetSection(nameof(Layout)).Get<Layout>();

        string mainConnection = connectionStrings.MainConnection;
        string headerLayout = layout.Header;
        string titleLayout = layout.Title;
        string footerLayout = layout.Footer;

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {mainConnection}");
        AnsiConsole.MarkupLine($"[cyan]Header:[/] {headerLayout}");
        AnsiConsole.MarkupLine($"[cyan]Title:[/] {titleLayout}");
        AnsiConsole.MarkupLine($"[cyan]Footer:[/] {footerLayout}");

    }

    /// <summary>
    /// Combines configuration data from multiple sources, including a JSON file and an in-memory collection,
    /// and displays the connection string and help desk information in the console.
    /// </summary>
    /// <remarks>
    /// This method reads configuration data from an "appsettings.json" file and a database. 
    /// The database settings are retrieved as a dictionary and added to the configuration using an in-memory collection.
    /// The combined configuration is then used to retrieve and display connection string and help desk details.
    /// </remarks>
    public static void Combination()
    {
            
        SpectreConsoleHelpers.Print();

        var settings = DataOperations.ReadFromDatabase();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(settings).Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        string mainConnection = connectionStrings.MainConnection;
        var helpDesk = configuration.GetSection(nameof(HelpDesk)).Get<HelpDesk>();
      
        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {mainConnection}");
        AnsiConsole.MarkupLine($"[cyan]Phone:[/] {helpDesk.Phone}");
        AnsiConsole.MarkupLine($"[cyan]Email:[/] {helpDesk.Email}");
    }
}