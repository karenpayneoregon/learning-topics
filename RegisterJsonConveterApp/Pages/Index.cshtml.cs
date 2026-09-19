using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RegisterJsonConveterApp.Classes;

namespace RegisterJsonConveterApp.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            Samples.GlobalExample();
        }
    }
}
