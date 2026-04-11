using Microsoft.Extensions.DependencyInjection;
using ProcessOrdersApp.Classes.Configuration;

namespace ProcessOrdersApp;
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        await Setup();
        Application.Run(new MainForm());
    }
    /// <summary>
    /// Setup for reading connection strings and entity settings from appsettings.json
    /// </summary>
    private static async Task Setup()
    {
        
        SetupLogging.Development();
        
        var services = Classes.Configuration.ApplicationConfiguration.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
        serviceProvider.GetService<SetupServices>()!.GetEntitySettings();
        serviceProvider.GetService<SetupServices>()!.GetFileSettings();
    }
}