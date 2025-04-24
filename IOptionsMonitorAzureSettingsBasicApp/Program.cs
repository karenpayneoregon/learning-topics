using IOptionsMonitorAzureSettingsBasicApp.Classes;
using IOptionsMonitorAzureSettingsBasicApp.Models;
using KellermanSoftware.CompareNetObjects;
using Serilog;

namespace IOptionsMonitorAzureSettingsBasicApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
        // Register CompareLogic with default options
        builder.Services.AddSingleton<CompareLogic>();

        // OR Register CompareLogic with custom configuration
        builder.Services.AddSingleton(sp => new CompareLogic
        {
            Config = new ComparisonConfig
            {
                IgnoreCollectionOrder = true, // Example configuration
                MaxDifferences = 10
            }
        });

        builder.Services.Configure<AzureSettings>(builder.Configuration.GetSection(AzureSettings.Settings));
        builder.Services.Configure<AzureSettings>(builder.Configuration.GetSection(nameof(AzureSettings)));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
