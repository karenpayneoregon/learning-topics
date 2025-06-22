using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace WebApplication1.Pages;
public class IndexModel : PageModel
{

    public IndexModel()
    {
    }

    public void OnGet()
    {
        Log.Information("OnGet method called.");
    }
}
