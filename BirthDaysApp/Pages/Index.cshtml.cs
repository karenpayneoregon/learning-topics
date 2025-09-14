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

    public List<BirthDayMonthGroup> GroupedBirthDays { get; private set; } = new();
    public void OnGet()
    {
        Log.Information("Greetings");

        BirthDays = context.BirthDays.ToList();

        GroupedBirthDays = BirthDays
            .OrderBy(b => b.BirthDate.Month)
            .ThenBy(b => b.BirthDate.Day)
            .GroupBy(b => b.BirthDate.Month)
            .Select(g => new BirthDayMonthGroup(
                g.Key,
                GlobalCulture.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                g.ToList()))
            .ToList();

        foreach (var day in BirthDays)
        {
            Console.WriteLine($"{day:Id}{day,-20:F}{day:Age}");
        }
    }
}
