using AuditInterceptorSampleApp.Classes.Interceptors;
using AuditInterceptorSampleApp.Models;
using ConsoleConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace AuditInterceptorSampleApp.Data;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        StandardLogging(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    /// <summary>
    /// Configures the database connection with standard logging settings.
    /// </summary>
    /// <param name="optionsBuilder">
    /// An instance of <see cref="DbContextOptionsBuilder"/> used to configure the database context options.
    /// </param>
    /// <remarks>
    /// This method enables sensitive data logging, adds an <see cref="AuditInterceptor"/> for auditing changes, 
    /// and logs messages to the debug output.
    /// </remarks>
    public static void StandardLogging(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder
            .UseSqlServer(AppConnections.Instance.MainConnection)
            .EnableSensitiveDataLogging()
            .AddInterceptors(new AuditInterceptor())
            .LogTo(message => Debug.WriteLine(message));

    }


    private static void SeriLogging(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder
            .UseSqlServer(ConnectionString())
            .EnableSensitiveDataLogging()
            .LogTo(Log.Logger.Information, LogLevel.Information, null)
            .EnableDetailedErrors();

    }
}