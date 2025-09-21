using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace TablesVsDivsSample1App.Pages;
public class IndexModel : PageModel
{
    public void OnGet()
    {
        Log.Information("Greetings");
    }
}
