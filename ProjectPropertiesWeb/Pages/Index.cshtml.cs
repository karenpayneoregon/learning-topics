using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPropertiesLibrary;
using ProjectPropertiesLibrary.Models;


namespace ProjectPropertiesWeb.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public Details Details { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    Details GetAllInfo() =>
        new()
        {
            Company = Info.GetCompany(),
            Copyright = Info.GetCopyright(),
            Product = Info.GetProduct(),
            Description = Info.GetDescription(),
            Version = Info.GetVersion().ToString()
        };
}
