using GetSettingFromAppSettings.Classes;
using GetSettingFromAppSettings.Models;
using Microsoft.Extensions.Options;

namespace GetSettingFromAppSettings;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();

        ConfigureServicesWithValidation(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

    private static void ConfigureServicesWithValidation(WebApplicationBuilder builder)
    {
        if (!builder.Configuration.GetSection("Logging").Exists())
        {
            throw new InvalidOperationException("Configuration section 'Logging' is missing.");
        }

        builder.Services
            .AddOptions<LoggingSettings>()
            .Bind(builder.Configuration.GetSection("Logging"))
            .ValidateOnStart();

        builder.Services
            .AddOptions<HelpDesk>()
            .Bind(builder.Configuration.GetSection("HelpDesk"))
            .ValidateOnStart();

        builder.Services.AddSingleton<IValidateOptions<LoggingSettings>, LoggingSettingsValidation>();
        builder.Services.AddSingleton<IValidateOptions<HelpDesk>, HelpdeskValidation>();
    }
}
