using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CustomIConfigurationSourceSample.Classes;
internal class DbContextOptions
{
    /// <summary>
    /// Creates and configures a new instance of <see cref="DbContextOptionsBuilder{TContext}"/> for the specified database context.
    /// </summary>
    /// <param name="connectionString">
    /// The connection string used to configure the database context.
    /// </param>
    /// <returns>
    /// An instance of <see cref="DbContextOptionsBuilder{TContext}"/> configured with the specified connection string
    /// and additional options such as logging and sensitive data logging.
    /// </returns>
    /// <remarks>
    /// In debug mode, the method logs database commands to the debug output. In release mode, it logs to a custom file logger.
    /// </remarks>
    public static DbContextOptionsBuilder<Context> DbContextOptionsBuilder(string connectionString)
    {   
        var options = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString)
#if DEBUG
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
#else
.LogTo(new DbContextToFileLogger().Log, [DbLoggerCategory.Database.Command.Name],
                        LogLevel.Information)
#endif
            .EnableSensitiveDataLogging();

        return options;

    }
}
