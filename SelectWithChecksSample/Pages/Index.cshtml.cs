using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SelectWithChecksSample.Classes;
using SelectWithChecksSample.Models;
using Spectre.Console;

namespace SelectWithChecksSample.Pages;
public class IndexModel : PageModel
{

    [BindProperty]
    public required List<CheckboxItem> Items { get; set; } = Lookups.BuildMonths();

    public void OnGet()
    {
        var pacific = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        int currentMonth = TimeZoneInfo.ConvertTime(DateTime.UtcNow, pacific).Month;

        foreach (var item in Items)
        {
            item.IsSelected = (item.Id == currentMonth);
        }
    }

    public PageResult OnPostButton1()
    {
        var selectedItems = Items.Where(item => item.IsSelected).ToList();
        foreach (var item in selectedItems)
        {
            AnsiConsole.MarkupLine($"[deeppink3_1]{item.Id, -4}[/][yellow]{item.Text}[/]");
        }
        
        return Page();
    }
}
