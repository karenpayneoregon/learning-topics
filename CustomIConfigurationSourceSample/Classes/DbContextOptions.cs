using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using EntityCoreFileLogger;
using Microsoft.Extensions.Logging;

namespace CustomIConfigurationSourceSample.Classes;
internal class DbContextOptions
{
    /// <summary>
    /// Configures and returns a new instance of <see cref="DbContextOptionsBuilder{TContext}"/> 
    /// for development purposes using the specified connection string.
    /// </summary>
    /// <param name="connectionString">
    /// The connection string to be used for configuring the database context.
    /// </param>
    /// <returns>
    /// An instance of <see cref="DbContextOptionsBuilder{TContext}"/> configured for development, 
    /// including logging to the debug output and enabling sensitive data logging.
    /// </returns>
    /// <remarks>
    /// This method is intended for use in development environments. It configures logging to output 
    /// database commands to the debug console and enables sensitive data logging for debugging purposes.
    /// </remarks>
    public static DbContextOptionsBuilder<Context> DbContextOptionsBuilderDevelopment(string connectionString)
    {   

        var options = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString)
            .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
            .EnableSensitiveDataLogging();

        return options;

    }

    /// <summary>
    /// Configures and returns a new instance of <see cref="DbContextOptionsBuilder{TContext}"/> 
    /// for production purposes using the specified connection string.
    /// </summary>
    /// <param name="connectionString">
    /// The connection string to be used for configuring the database context.
    /// </param>
    /// <returns>
    /// An instance of <see cref="DbContextOptionsBuilder{TContext}"/> configured for production, 
    /// including logging database commands to a file.
    /// </returns>
    /// <remarks>
    /// This method is intended for use in production environments. It configures logging to output 
    /// database commands to a file using a custom logger and does not enable sensitive data logging.
    /// </remarks>
    public static DbContextOptionsBuilder<Context> DbContextOptionsBuilderProduction(string connectionString)
    {

        var options = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString)
            .LogTo(new DbContextToFileLogger().Log, [DbLoggerCategory.Database.Command.Name], 
                LogLevel.Information);

        return options;

    }
}
