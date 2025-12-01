using CustomTagHelpersApp.Classes;
using FooterLibrary;

namespace CustomTagHelpersApp;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Bind FooterDetails from configuration
        builder.Services.Configure<FooterDetails>(
            builder.Configuration.GetSection(nameof(FooterDetails)));
        
        builder.Services.AddRazorPages();
        SetupLogging.Development();
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
