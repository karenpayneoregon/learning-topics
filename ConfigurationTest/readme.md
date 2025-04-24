# About


```csharp
using System.Runtime.CompilerServices;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.Extensions.DependencyInjection;


// ReSharper disable once CheckNamespace
namespace ConfigurationTest;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);

        Setup();


    }
    private static void Setup()
    {
        var services = ApplicationConfiguration.ConfigureServices();
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
        serviceProvider.GetService<SetupServices>()!.GetEntitySettings();
    }

}

```
