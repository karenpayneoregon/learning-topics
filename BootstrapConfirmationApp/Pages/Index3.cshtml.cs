using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BootstrapConfirmationApp.Pages
{
    
    public class Index3Model : PageModel
    {
        public string Title { get; set; } = "FYI";
        public string Message { get; set; } = "Your account has been updated.";
        public string ButtonText { get; set; } = "OK";

        public void OnGet()
        {
        }
    }
}
