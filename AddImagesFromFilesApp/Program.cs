using AddImagesFromFilesApp.Classes.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using static System.DateTime;

namespace AddImagesFromFilesApp;
internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();
        await Setup();
        Application.Run(new MainForm());
    }
    /// <summary>
    /// Setup for reading connection strings and entity settings from appsettings.json
    /// </summary>
    private static async Task Setup()
    {
        var services = Classes.Configuration.ApplicationConfiguration.ConfigureServices();
        await using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<SetupServices>()!.GetConnectionStrings();
        serviceProvider.GetService<SetupServices>()!.GetEntitySettings();
        
        Development();
        
    }
    
    /// <summary>
    /// Configures the logging mechanism for the application during development.
    /// </summary>
    /// <remarks>
    /// This method initializes a logger using Serilog, which writes log entries to a file.
    /// The log file is stored in a directory named "LogFiles" within the application's base directory.
    /// The log file name is dynamically generated based on the current date.
    /// </remarks>
    public static void Development()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{Now.Year}-{Now.Month:D2}-{Now.Day:D2}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}