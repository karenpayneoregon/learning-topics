using FluentValidation;
using FluentWebApplication.Classes;
using FluentWebApplication.Data;
using FluentWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;
using FluentWebApplication.Models.Configuration;

namespace FluentWebApplication;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        //builder.Services.AddScoped<IValidator<Person>, PersonValidator>();
        //builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<PersonValidator>();


        if (builder.Environment.IsDevelopment())
        {
            // colorize output
            builder.Host.UseSerilog((_, configuration) =>
                configuration.WriteTo.Console(theme: AnsiConsoleTheme.Code));
            
            builder.Services.AddDbContextPool<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging()
                    .LogTo(new DbContextToFileLogger().Log, [DbLoggerCategory.Database.Command.Name],
                        LogLevel.Information));
        }else
        {
            builder.Services.AddDbContextPool<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
        
        builder.Services
            .AddOptions<CancellationTokenSettings>()
            .Bind(builder.Configuration.GetSection(CancellationTokenSettings.SectionName))
            .Validate(s => s.Timeout >= 0, "Timeout must be >= 0")
            .ValidateOnStart();

        builder.Services.AddHostedService<EntityCoreWarmupService>();
        
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
