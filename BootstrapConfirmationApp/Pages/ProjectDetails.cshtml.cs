using CommonLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

using static CommonLibrary.ProjectInformation;

namespace BootstrapConfirmationApp.Pages
{
    public class ProjectDetailsModel : PageModel
    {
        public required ProjectDetails Details { get; set; } = new()
        {
            Copyright = GetCopyright(),
            ProjectName = GetProduct(),
            Company = GetCompany(),
            Version = GetVersion()
        };

        public void OnGet()
        {
     
        }
    }
}
