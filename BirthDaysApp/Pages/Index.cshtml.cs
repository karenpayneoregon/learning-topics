using BirthDaysApp.Classes;
using BirthDaysApp.Data;
using BirthDaysApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Globalization;

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

        var people = BirthDays.Select(b =>
            new PersonViewModel(
                b.FirstName,
                b.LastName,
                b.BirthDate,
                b.YearsOld ?? b.BirthDate.GetAge()  // compute if missing
            )
        );

        GroupedBirthDays = people
            .OrderBy(p => p.BirthDate.Month)
            .ThenBy(p => p.BirthDate.Day)
            .GroupBy(p => p.BirthDate.Month)
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
