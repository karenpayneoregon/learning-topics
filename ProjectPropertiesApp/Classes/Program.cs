using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using ProjectPropertiesApp.Classes;
using ProjectPropertiesLibrary.Models;
using System.Reflection;
using System.Runtime.CompilerServices;
using ProjectPropertiesLibrary;
using Spectre.Console;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;
using ProjectPropertiesApp.Classes.Core;

// ReSharper disable once CheckNamespace
namespace ProjectPropertiesApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        SetupLogging.Development();
        Setup();

    }
    private static void Setup()
    {
        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup.GetConnectionStrings();
        setup.GetEntitySettings();
        SpectreConsoleHelpers.SetEncoding();
    }

    internal static void ShowDetails()
    {
        var table = new Table()
            .RoundedBorder()
            .BorderColor(Color.Pink1)
            .Title("[yellow bold]Information[/]");

        table.AddColumn("[yellow bold]Attribute[/]");
        table.AddColumn("[yellow bold]Value[/]");

        var details = GetAllInfo();
        table.AddRow("[cyan]Product[/]", details.Product);
        table.AddRow("[cyan]Version[/]", details.Version);
        table.AddRow("[cyan]Build Date[/]", details.BuildDate);
        table.AddRow("[cyan]Copyright[/]", details.Copyright);
        table.AddRow("[cyan]Company[/]", details.Company);
        table.AddRow("[cyan]Description[/]", details.Description);
        AnsiConsole.Write(table);
        

        Details GetAllInfo()
        {
            return new Details()
            {
                Company = Info.GetCompany(),
                Copyright = Info.GetCopyright(),
                BuildDate = Info.GetBuildDate()!,
                Product = Info.GetProduct(),
                Description = Info.GetDescription(),
                Version = Info.GetVersion().ToString(),
            };
        }
    }
}
