using ConsoleConfigurationLibrary.Classes;
using ConsoleHelperLibrary.Classes;
using JsonDictionaryStringObjectConverterSample.Classes.Configuration;
using JsonDictionaryStringObjectConverterSample.Classes.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

// ReSharper disable once CheckNamespace
namespace JsonDictionaryStringObjectConverterSample;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        var product = Assembly.GetEntryAssembly()?
            .GetCustomAttribute<AssemblyProductAttribute>()?
            .Product;

        Console.Title = product!;

        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        Setup();

    }
    private static void Setup()
    {

        SetupLogging.Development();

        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<SetupServices>();
        setup.GetConnectionStrings();
        setup.GetEntitySettings();

        SpectreConsoleHelpers.SetEncoding();
    }
}
