using System.Data;
using System.Text.Json;
using AddInMemoryCollectionSample.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Layout = AddInMemoryCollectionSample.Models.Layout; // resolves ambiguity with Spectre.Console Layout

namespace AddInMemoryCollectionSample.Classes;

internal class Examples
{

    /// <summary>
    /// Demonstrates the conventional approach to reading configuration settings 
    /// from an appsettings.json file and displaying them using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method retrieves connection strings and settings from the database 
    /// and displays them in the console. It uses Dapper for database interaction 
    /// and Spectre.Console for formatted console output.
    /// </remarks>
    public static void Conventional()
    {
        SpectreConsoleHelpers.Print();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

        using IDbConnection db = new SqlConnection(connectionStrings.MainConnection);
        string sql = """
             SELECT Section + ':' + [Key] AS [Key], Value 
             FROM dbo.Settings;
             """;
        var dictionary = db.Query<(string Key, string Value)>(sql)
            .ToDictionary(x => x.Key, x => x.Value);

        foreach (var kvp in dictionary)
        {
            AnsiConsole.MarkupLine($"[hotpink3]Key:[/] {kvp.Key}, [hotpink3]Value:[/] {kvp.Value}");
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {connectionStrings.MainConnection}");

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

    public static void DryRun4()
    {

        SpectreConsoleHelpers.Print();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings1.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(new Dictionary<string, string>
            {
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

    public static void DryRun5()
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings1.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(SettingsReader.LoadLayout()) // Inject the layout.json content
            .Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        var layout = configuration.GetSection(nameof(Layout)).Get<Layout>();

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {connectionStrings.MainConnection}");
        AnsiConsole.MarkupLine($"[cyan]Header:[/] {layout.Header}");
        AnsiConsole.MarkupLine($"[cyan]Title:[/] {layout.Title}");
        AnsiConsole.MarkupLine($"[cyan]Footer:[/] {layout.Footer}");

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

        Console.WriteLine();
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
            .AddInMemoryCollection(settings)
            .Build();

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        string mainConnection = connectionStrings.MainConnection;
        var helpDesk = configuration.GetSection(nameof(HelpDesk)).Get<HelpDesk>();

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {mainConnection}");
        AnsiConsole.MarkupLine($"[cyan]Phone:[/] {helpDesk.Phone}");
        AnsiConsole.MarkupLine($"[cyan]Email:[/] {helpDesk.Email}");

        Console.WriteLine();
    }

    /// <summary>
    /// Demonstrates the basic usage of in-memory and JSON configuration sources 
    /// to populate and retrieve application settings, such as company details 
    /// and connection strings.
    /// </summary>
    /// <remarks>
    /// This method combines configuration from an in-memory collection and a JSON file 
    /// (e.g., "appsettings.json"). It retrieves and displays company settings, such as 
    /// name, address, city, and state, as well as connection string details.
    /// </remarks>
    public static void CompanySettingsBasic()
    {
        SpectreConsoleHelpers.Print();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "CompanySettings:Name", "Contoso" },
                { "CompanySettings:Address", "123 Main St" },
                { "CompanySettings:City", "Seattle" },
                { "CompanySettings:State", "WA" }
            }).Build();

        var companySettings = configuration.GetSection(nameof(CompanySettings)).Get<CompanySettings>();

        AnsiConsole.MarkupLine($"[cyan]Name:[/] {companySettings.Name}");
        AnsiConsole.MarkupLine($"[cyan]Address:[/] {companySettings.Address}");
        AnsiConsole.MarkupLine($"[cyan]City:[/] {companySettings.City}");
        AnsiConsole.MarkupLine($"[cyan]State:[/] {companySettings.State}");

        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {connectionStrings.MainConnection}");

        Console.WriteLine();

    }

    /// <summary>
    /// Demonstrates the use of strongly-typed configuration settings in .NET Core.
    /// </summary>
    /// <remarks>
    /// This method showcases how to configure and retrieve application settings using:
    /// <list type="bullet">
    /// <item><description>In-memory collections</description></item>
    /// <item><description>JSON configuration files</description></item>
    /// <item><description>Dependency injection with <see cref="IOptions{TOptions}"/></description></item>
    /// </list>
    /// The method retrieves and displays company settings and connection strings.
    /// </remarks>
    public static void CompanySettings()
    {
        SpectreConsoleHelpers.Print();

        var companyData = SettingsReader.CompanySettingsList();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(companyData)
            .Build();

        // Set up dependency injection
        var services = new ServiceCollection();
        services.Configure<CompanySettings>(configuration.GetSection("Company"));
        var serviceProvider = services.BuildServiceProvider();


        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

        AnsiConsole.MarkupLine($"[cyan]Connection String:[/] {connectionStrings.MainConnection}");

        // Resolve the strongly-typed settings
        var companySettings = serviceProvider.GetRequiredService<IOptions<CompanySettings>>().Value;

        AnsiConsole.MarkupLine($"[cyan]Company Name:[/] {companySettings.Name}");
        AnsiConsole.MarkupLine($"[cyan]Company Address:[/] {companySettings.Address}");
        AnsiConsole.MarkupLine($"[cyan]Company City:[/] {companySettings.City}");
        AnsiConsole.MarkupLine($"[cyan]Company State:[/] {companySettings.State}");

        Console.WriteLine();
    }
}