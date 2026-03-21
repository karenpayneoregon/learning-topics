using GetSettingFromAppSettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace GetSettingFromAppSettings.Pages;
public class IndexModel(IOptions<LoggingSettings> options) : PageModel
{
    private readonly LoggingSettings _logging = options.Value;

    public string? Default { get; private set; }
    public string? AspNetCore { get; private set; }
    public string? EfCommand { get; private set; }

    public void OnGet()
    {
        Default = _logging.LogLevel.Default;
        AspNetCore = _logging.LogLevel.MicrosoftAspNetCore;
        EfCommand = _logging.LogLevel.MicrosoftEntityFrameworkCoreDatabaseCommand;
    }
}
