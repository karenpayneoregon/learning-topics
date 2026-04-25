using ProcessOrdersApp.Interfaces;
using ProcessOrdersApp.Models;
using Serilog;
using Serilog.Filters;
using static System.DateTime;

namespace ProcessOrdersApp.Classes.Configuration;

/// <summary>
/// Provides functionality for configuring and setting up logging within the application.
/// </summary>
/// <remarks>
/// This class is responsible for initializing and configuring the logging mechanism using Serilog.
/// It ensures that log files are created with a specific format and stored in a designated directory.
/// </remarks>
public class SetupLogging
{
    /// <summary>
    /// Configures the logging mechanism for the development environment.
    /// </summary>
    /// <remarks>
    /// This method sets up Serilog to write log entries to a file. The log file is stored in a directory
    /// named "LogFiles" within the application's base directory. The file name includes the current date
    /// (year, month, and day) to ensure logs are organized by date.
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

