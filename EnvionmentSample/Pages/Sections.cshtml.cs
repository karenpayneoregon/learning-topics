using EnvironmentApplication.Classes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnvironmentApplication.Pages
{
    public class SectionsModel : PageModel
    {
        public string CurrentPageName { get; private set; } = null!;

        public void OnGet()
        {
            CurrentPageName = PageHelpers.GetCurrentPageName(Request);
            Console.WriteLine(CurrentPageName);
        }
    }
}
