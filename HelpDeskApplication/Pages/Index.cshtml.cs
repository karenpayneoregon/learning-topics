using HelpDeskApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;

namespace HelpDeskApplication.Pages;
public class IndexModel : PageModel
{
    private readonly IOptionsSnapshot<HelpDesk> _helpdeskSnapshot;

    public IndexModel(IOptionsSnapshot<HelpDesk> helpdeskSnapshot)
    {
        _helpdeskSnapshot = helpdeskSnapshot;
    }
    public void OnGet()
    {
    }
}
