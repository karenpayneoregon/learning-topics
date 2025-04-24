using System.Runtime.CompilerServices;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;


// ReSharper disable once CheckNamespace
namespace ConfigurationTest;

internal partial class Program
{
    [ModuleInitializer]
    public static void Initialize()
    {
        Console.Title = "TODO";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        Setup();
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
