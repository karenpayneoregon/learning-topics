using HelpDeskApplication.Classes;
using HelpDeskApplication.Models;
using Microsoft.Extensions.Options;
using Serilog;
using RequiredDirectories = HelpDeskApplication.Classes.RequiredDirectories;

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

        var fileSettings = builder.Configuration.GetSection(nameof(FileSettings)).Get<FileSettings>();

        // Define the path where key-per-file secrets are stored
        var secretsPath = fileSettings.SecretsDirectory;

        if (Directory.Exists(secretsPath))
        {
            builder.Configuration.AddKeyPerFile(secretsPath, optional: true, reloadOnChange: true);
        }

        builder.Services.Configure<DatabaseSettings>(options =>
        {
            options.DatabasePassword = builder.Configuration[nameof(DatabaseSettings.DatabasePassword)] ?? "???";
        });




        // Bind configuration section to FileSettings
        builder.Services.AddOptions<RequiredDirectories>()
            .Bind(builder.Configuration.GetSection(nameof(FileSettings))); 

        // using IValidateOptions<T>:
        builder.Services.AddSingleton<IValidateOptions<RequiredDirectories>, DirectoryOptionsValidation>();
        builder.Services.AddOptions<RequiredDirectories>()
            .ValidateOnStart();

        
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