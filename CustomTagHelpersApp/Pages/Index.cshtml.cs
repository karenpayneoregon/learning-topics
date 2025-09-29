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
    public required string FirstName { get; set; }

    public void OnGet()
    {
        FirstName = "John";
    }
    public void OnPost()
    {
        Console.WriteLine(new string('_', 50));

        Log.Information("Selected week day {WeekDay} {Name}",WeekDay.Actual(), WeekDay.GetDisplayName());


        if (!string.IsNullOrWhiteSpace(FirstName))
        {
            Log.Information("First name is {FirstName}", FirstName);
        }
        else
        {
            Log.Information("First name is not provided");
        }
    }
}
