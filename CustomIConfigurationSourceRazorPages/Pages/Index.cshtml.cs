
using ConsoleConfigurationLibrary.Models;
using CustomIConfigurationSourceRazorPages.Classes;
using CustomIConfigurationSourceRazorPages.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618, CS9264

namespace CustomIConfigurationSourceRazorPages.Pages;

public class IndexModel : PageModel
{

    private readonly IConfiguration _configuration;
    private readonly Context _context;
    public string ConfigValue { get; private set; }
    public IndexModel(IConfiguration configuration, Context context)
    {
        _configuration = configuration;
        _context = context;

        var sss = Config.Configuration.JsonRoot().GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        var ccc = sss.MainConnection;


    }

    public void OnGet()
    {
        HelpDesk helpDesk = DataOperations.ReadFromDatabase(_context);
    }
}
