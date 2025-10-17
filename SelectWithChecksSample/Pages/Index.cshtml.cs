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

    /// <summary>
    /// Handles the GET request for the page.
    /// </summary>
    /// <remarks>
    /// This method initializes the <see cref="Items"/> property with a list of months
    /// and sets the <see cref="CheckboxItem.IsSelected"/> property to <c>true</c> for the current month
    /// based on the Pacific Standard Time zone.
    /// </remarks>
    public void OnGet()
    {
        var pacific = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
        var currentMonth = TimeZoneInfo.ConvertTime(DateTime.UtcNow, pacific).Month;

        foreach (var item in Items)
        {
            item.IsSelected = (item.Id == currentMonth);
        }
    }

    /// <summary>
    /// Handles the POST request for the "Button1" action.
    /// </summary>
    /// <remarks>
    /// This method processes the selected items from the <see cref="Items"/> property,
    /// outputs their details to the console using Spectre.Console, and returns the current page.
    /// </remarks>
    /// <returns>
    /// A <see cref="PageResult"/> representing the current page.
    /// </returns>
    public PageResult OnPostButton1()
    {
        var selectedItems = Items.Where(item => item.IsSelected).ToList();
        if (selectedItems.Any())
        {
            foreach (var item in selectedItems)
            {
                AnsiConsole.MarkupLine($"[deeppink3_1]{item.Id,-4}[/][yellow]{item.Text}[/]");
            }
        }else
        {
            AnsiConsole.MarkupLine("[deeppink3_1]No items selected[/]");
        }


        return Page();
    }
}
