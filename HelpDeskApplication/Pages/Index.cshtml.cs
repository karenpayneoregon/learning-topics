using HelpDeskApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;

namespace HelpDeskApplication.Pages;
public class IndexModel(IOptionsSnapshot<HelpDesk> helpdeskSnapshot, IOptions<DatabaseSettings> options)
    : PageModel
{
    public string DatabasePassword { get; private set; } = "Not Found";

    private readonly IOptionsSnapshot<HelpDesk> _helpdeskSnapshot = helpdeskSnapshot;
    private readonly DatabaseSettings _databaseSettings = options.Value;

    public void OnGet()
    {
        DatabasePassword = _databaseSettings.DatabasePassword;
    }
}


