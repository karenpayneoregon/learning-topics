using EntityCoreFileLogger;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TablesVsDivsSample1App.Classes;
using TablesVsDivsSample1App.Data;

namespace TablesVsDivsSample1App;
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
            .WriteTo.Debug()
            .CreateLogger();

        builder.Host.UseSerilog();

        // assist with getting caller in edit page
        builder.Services.AddHttpContextAccessor();

        // Register DbContext
        builder.Services.AddDbContext<Context>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("MainConnection"))
            .LogTo(new DbContextToFileLogger().Log, 
            [DbLoggerCategory.Database.Command.Name], LogLevel.Information));

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
}
