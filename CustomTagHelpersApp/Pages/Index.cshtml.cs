using CustomTagHelpersApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace CustomTagHelpersApp.Pages;
public class IndexModel : PageModel
{
    [BindProperty]
    public WeekDays WeekDay { get; set; }

    [BindProperty]
    public string FirstName { get; set; }

    public void OnGet()
    {
    }
    public void OnPost()
    {
        Log.Information("Selected week day {WeekDay}", (int)WeekDay +1);
    }
}
