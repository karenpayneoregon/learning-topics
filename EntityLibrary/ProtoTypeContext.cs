using System.Diagnostics;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace EntityLibrary;
/// <summary>
/// Represents the base database context for Entity Framework Core, providing shared functionality 
/// and configuration for derived contexts.
/// </summary>
/// <remarks>
/// This abstract class is designed to be inherited by specific database context implementations. 
/// It includes default configuration for connecting to a SQL Server database and enables sensitive 
/// data logging in development environments.
/// </remarks>
public abstract class ProtoTypeContext() : DbContext(BuildOptions())
{
    private static DbContextOptions BuildOptions()
    {
        var builder = new DbContextOptionsBuilder()
            .UseSqlServer(AppConnections.Instance.MainConnection);

        // NOTE - adjust based on your environment variable setup
        var env = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT") ?? "Production";

        if (env.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            builder.EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        return builder.Options;
    }
}




