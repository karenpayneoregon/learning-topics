using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceRazorPages.Classes;
using CustomIConfigurationSourceRazorPages.Models;
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
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        // Register MemoryCache
        builder.Services.AddMemoryCache();

        // Register DbContext
        builder.Services.AddDbContext<Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.MainConnection))));

        using var context = new Context();
        var helpDesk = DataOperations.GetHelpDeskValues(context);

        // Add Configuration Sources

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("other.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<MailSettings>()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                {"Helpdesk:phone", helpDesk.Phone},
                {"Helpdesk:email", helpDesk.Email}
            });

        
        //  Build service provider early to resolve DI services
        var serviceProvider = builder.Services.BuildServiceProvider();

        //  Add Database Configuration Source before final build
        configurationBuilder.Add(new DatabaseConfigurationSource(serviceProvider, configurationBuilder.Build()));

        //  Build configuration and register it in DI
        var configuration = configurationBuilder.Build();
        builder.Services.AddSingleton<IConfiguration>(configuration); // ✅ Correct way

        // Add services to the container
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
