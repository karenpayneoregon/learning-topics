using Serilog;
using System;
using System.IO;
using static System.DateTime;

namespace Publisher.Classes;
public class SetupLogging
{
    /// <summary>
    /// Configures the logging for the development environment.
    /// </summary>
    /// <remarks>
    /// This method initializes a Serilog logger that writes log entries to a file.
    /// The log file is created in a directory named "LogFiles" within the application's base directory.
    /// The file name includes the current date in the format "YYYY-MM-DD/Log.txt".
    /// </remarks>
    public static void Development()
    {

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{Now.Year}-{Now.Month:d2}-{Now.Day:d2}", "Log.txt"),
                rollingInterval: RollingInterval.Infinite,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
    }
}
