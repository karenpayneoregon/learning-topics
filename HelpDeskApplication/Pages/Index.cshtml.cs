using HelpDeskApplication.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Serilog;

namespace HelpDeskApplication.Pages;
public class IndexModel : PageModel
{
    public string DatabasePassword { get; private set; } = "Not Found";

    private readonly IOptionsSnapshot<HelpDesk> _helpdeskSnapshot;
    private readonly DatabaseSettings _databaseSettings;
    public IndexModel(IOptionsSnapshot<HelpDesk> helpdeskSnapshot, IOptions<DatabaseSettings> options)
    {
        _helpdeskSnapshot = helpdeskSnapshot;
        _databaseSettings = options.Value;

    }
    public void OnGet()
    {
        DatabasePassword = _databaseSettings.DatabasePassword;
    }
}
