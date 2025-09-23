using BaseContextExampleApp.Classes;
using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace BaseContextExampleApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var assembly = Assembly.GetEntryAssembly();
        var product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
        var description = assembly?.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        SetupLogging.Development();
        Setup();

        // See ProtoTypeContext
        var dict = Environment.GetEnvironmentVariables();
        if (!dict.Contains("CONSOLE_ENVIRONMENT"))
        {
            AnsiConsole.MarkupLine("[bold red]Please set the environment variable[/] [yellow]CONSOLE_ENVIRONMENT[/]");
            SpectreConsoleHelpers.ExitPrompt();
            return;
        }

    }
    private static void Setup()
    {
        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup.GetConnectionStrings();
        setup.GetEntitySettings();
    }
}
