using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace CustomTagHelpersApp.Pages;
public class IndexModel : PageModel
{
    public void OnGet()
    {
        Log.Information("Greetings");
    }
}
