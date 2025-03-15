
using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceRazorPages.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable CS8618, CS9264

namespace CustomIConfigurationSourceRazorPages.Pages;

public class IndexModel : PageModel
{

    private readonly IConfiguration _configuration;

    /// <summary>
    /// Gets the mail-related configuration settings for the application.
    /// </summary>
    /// <remarks>
    /// This property retrieves its value from the application's configuration, specifically from the 
    /// "MailSettings" section. It includes details such as the sender's email address, SMTP host, 
    /// port, timeout, and the folder for storing email pickups. The settings are stored in secrets.
    /// </remarks>
    public MailSettings MailSettings { get; private set; }
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

        // non-secrets
        var connectionSection = Config.Configuration.JsonRoot()
            .GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

        var mainConnection = connectionSection!.MainConnection;

    }
    
    /// <summary>
    /// Handles GET requests for the Index Razor Page.
    /// </summary>
    /// <remarks>
    /// This method retrieves layout-related configuration settings, such as the page title, 
    /// and assigns them to the <see cref="PageModel.ViewData"/> dictionary for rendering in the Razor Page.
    /// Additionally, it fetches mail-related settings from the application's configuration 
    /// and assigns them to the <see cref="MailSettings"/> property which is stored in user secrets.
    /// </remarks>
    public void OnGet()
    {
        // non-secrets
        var layoutSection = _configuration.GetSection(nameof(Layout)).Get<Layout>();
        var title = layoutSection?.Title ?? "Home page"; 

        ViewData["Title"] = title;

        // in secrets
        MailSettings = _configuration.GetSection(nameof(MailSettings)).Get<MailSettings>();

    }
}
