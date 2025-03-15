using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceRazorPages.Classes;
using CustomIConfigurationSourceRazorPages.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Memory;
using Serilog;
#pragma warning disable ASP0000

namespace CustomIConfigurationSourceRazorPages;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        // Register DbContext
        builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString(nameof(ConnectionStrings.MainConnection))));

        var configurationBuilder = SetupCustomConfiguration();

        //  Build service provider
        var serviceProvider = builder.Services.BuildServiceProvider();

        //  Build configuration and register it in DI
        var configuration = configurationBuilder.Build();
        builder.Services.AddSingleton<IConfiguration>(configuration);

        builder.Services.AddRazorPages();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapStaticAssets();
        app.MapRazorPages().WithStaticAssets();
        app.Run();
    }

    /// <summary>
    /// Configures a custom <see cref="IConfigurationBuilder"/> by adding various configuration sources.
    /// </summary>
    /// <remarks>
    /// This method sets up the configuration builder with multiple sources, including:
    /// <list type="bullet">
    /// <item><description>JSON files such as "appsettings.json" and "other.json".</description></item>
    /// <item><description>User secrets for the <see cref="MailSettings"/> class.</description></item>
    /// <item><description>An in-memory configuration source populated with data from <see cref="DataOperations.GetHelpDeskValues"/>.</description></item>
    /// </list>
    /// Additionally, it demonstrates how to retrieve a specific <see cref="MemoryConfigurationSource"/> from the builder's sources.
    /// </remarks>
    /// <returns>An instance of <see cref="IConfigurationBuilder"/> configured with the specified sources.</returns>
    private static IConfigurationBuilder SetupCustomConfiguration()
    {
        /*
         * An alternate to AddInMemoryCollection in ConfigurationBuilder
         */
        var memorySource = new MemoryConfigurationSource { InitialData = DataOperations.GetHelpDeskValues() };

        // Add Configuration Sources
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("other.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<MailSettings>()
            .Add(memorySource);


        /*
         * Find the MemoryConfigurationSource instance in the ConfigurationBuilder
         * For demonstration purposes only
         */
        var memoryConfigurationSource = configurationBuilder.Sources
            .OfType<MemoryConfigurationSource>()
            .FirstOrDefault();


        return configurationBuilder;
    }
}
