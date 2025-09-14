using BirthDaysApp.Data;
using BirthDaysApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace BirthDaysApp.Pages;
public class IndexModel(Context context) : PageModel
{
    [BindProperty]
    public required List<BirthDay> BirthDays { get; set; }
    public void OnGet()
    {
        Log.Information("Greetings");

        BirthDays = context.BirthDays.ToList();

        foreach (var day in BirthDays)
        {
            Console.WriteLine($"{day:Id}{day,-20:F}{day:Age}");
        }
    }
}
