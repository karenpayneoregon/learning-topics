
using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceRazorPages.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable CS8618, CS9264

namespace CustomIConfigurationSourceRazorPages.Pages;

public class IndexModel : PageModel
{

    private readonly IConfiguration _configuration;
    private readonly Context _context;


    /// <summary>
    /// Initializes a new instance of the <see cref="IndexModel"/> class.
    /// </summary>
    /// <param name="configuration">
    /// An instance of <see cref="IConfiguration"/> used to access application configuration settings.
    /// </param>
    /// <param name="context">
    /// An instance of <see cref="Context"/> used for database operations.
    /// </param>
    /// <remarks>
    /// This constructor sets up the configuration and database context for the Razor Page.
    /// It also retrieves the main connection string from the application's configuration.
    /// </remarks>
    public IndexModel(IConfiguration configuration, Context context)
    {
        _configuration = configuration;
        _context = context;

        var section1 = Config.Configuration.JsonRoot().GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        var mainConnection = section1!.MainConnection;
    }



    public void OnGet()
    {
        var section2 = _configuration.GetSection(nameof(Layout)).Get<Layout>();
        var title = section2?.Title ?? "Home page"; 

        ViewData["Title"] = title; // Store title in ViewData
    }
}
