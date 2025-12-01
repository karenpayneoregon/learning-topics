using FooterLibrary;

namespace AppFooterTagHelperSample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Bind FooterDetails from appsettings.json
        builder.Services.Configure<FooterDetails>(
            builder.Configuration.GetSection(nameof(FooterDetails)));
        
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
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
