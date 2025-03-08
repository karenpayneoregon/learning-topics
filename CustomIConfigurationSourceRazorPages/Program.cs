using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceRazorPages.Classes;
using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;
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
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)  // Suppress ASP.NET Core logs
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)  // Suppress System logs
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        // Add Serilog to the logging pipeline
        builder.Host.UseSerilog();

        // Register MemoryCache
        builder.Services.AddMemoryCache();

        // Register DbContext
        builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.MainConnection))));

        using var context = new Context();
        var helpDesk = DataOperations.GetHelpDeskValues(context);

        // Add values from SQL-Server which are used in _Layout.cshtml
        var configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                {"Helpdesk:phone", helpDesk.Phone},
                {"Helpdesk:email", helpDesk.Email}
            })
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        // Build service provider early to resolve DI services
        var serviceProvider = builder.Services.BuildServiceProvider();

        // Add Database Configuration Source before building final configuration
        configurationBuilder.Add(new DatabaseConfigurationSource(serviceProvider, configurationBuilder.Build()));

        // Use the final configuration to register it with the dependency injection container
        IConfiguration configuration = configurationBuilder.Build();
        builder.Services.AddSingleton<IConfiguration>(configuration); // Register IConfiguration in DI

        
        // Add services to the container.
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
}
