using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SingletonWebApp.Classes;
using SingletonWebApp.Models;

namespace SingletonWebApp.Pages;

public class IndexModel : PageModel
{
    public List<MonthItem> MonthItems { get; set; } = [];

    [BindProperty]
    public int? SelectedMonth { get; set; }

    public MonthItem? SelectedMonthItem { get; set; }

    public void OnGet()
    {
        MonthItems = Helpers.Instance.MonthList;
    }

    public void OnPost()
    {
        MonthItems = Helpers.Instance.MonthList;

        SelectedMonthItem = MonthItems
            .FirstOrDefault(month => month.Index == SelectedMonth);
    }
}