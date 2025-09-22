using EntityCoreFileLogger;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using TablesVsDivsSample1App.Data;

namespace TablesVsDivsSample1App;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        // Enable FluentValidation's automatic validation
        builder.Services.AddFluentValidationAutoValidation();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.Debug()
            .CreateLogger();

        builder.Host.UseSerilog();

        // assist with getting caller in edit page
        builder.Services.AddHttpContextAccessor();

        RegisterDbContextAndLogFiles(builder, out var logDir);

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }

    /// <summary>
    /// Configures the application's database context and sets up the directory for log files.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="WebApplicationBuilder"/> used to configure the application's services and environment.
    /// </param>
    /// <param name="logDir">
    /// An output parameter that provides the path to the directory where log files will be stored.
    /// </param>
    private static void RegisterDbContextAndLogFiles(WebApplicationBuilder builder, out string logDir)
    {
        // Register DbContext
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(new DbContextToFileLogger().Log,
                    [DbLoggerCategory.Database.Command.Name], LogLevel.Information));
        }
        else
        {
            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection"))
                .LogTo(new DbContextToFileLogger().Log,
                    [DbLoggerCategory.Database.Command.Name], LogLevel.Information));
        }

        logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", DateTime.Now.ToString("yyyy-MM-dd"));
        if (!Directory.Exists(logDir))
        {
            Directory.CreateDirectory(logDir);
        }
    }
}
