namespace MultipleSubmitButtons1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        /*
         * For reading configuration values into a Dictionary<string, string> from appsettings.json
         * for buttons and their corresponding program names.
         */
        builder.Services.Configure<Dictionary<string, string>>(builder.Configuration.GetSection("Programs"));

        builder.Services.AddRazorPages();

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

        app.MapFallback(context => {
            context.Response.Redirect("/NotFound");
            return Task.CompletedTask;
        });

        app.Run();
    }
}
