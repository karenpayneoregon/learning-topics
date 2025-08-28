using Microsoft.Extensions.DependencyInjection;
using static ConsoleConfigurationLibrary.Classes.ApplicationConfiguration;

namespace TableDependenciesSample;
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Setup();
        Application.Run(new Form1());
    }
    /// <summary>
    /// Configures and initializes the necessary services for the application.
    /// </summary>
    /// <remarks>
    /// This method sets up the dependency injection container, builds the service provider,
    /// and invokes methods to retrieve connection strings and entity settings.
    /// </remarks>
    private static void Setup()
    {
        var services = ConfigureServices();
        using var provider = services.BuildServiceProvider();
        var setup = provider.GetService<Config.SetupServices>();
        setup.GetConnectionStrings();
        setup.GetEntitySettings();
    }
}