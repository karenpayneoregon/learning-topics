using HelpDeskApplication.Models;
using Serilog;

namespace HelpDeskApplication;
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

        builder.Services.AddRazorPages();

        builder.Services.Configure<HelpDesk>(builder.Configuration.GetSection(nameof(HelpDesk)));

        // Define the path where key-per-file secrets are stored
        var secretsPath = "C:\\OED\\Secrets"; 

        if (Directory.Exists(secretsPath))
        {
            builder.Configuration.AddKeyPerFile(secretsPath, optional: true, reloadOnChange: true);
        }

        builder.Services.Configure<DatabaseSettings>(options =>
        {
            options.DatabasePassword = builder.Configuration[nameof(DatabaseSettings.DatabasePassword)] ?? "???";
        });

        var app = builder.Build();
        


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}