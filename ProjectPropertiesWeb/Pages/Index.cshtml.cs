using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPropertiesLibrary;
using ProjectPropertiesLibrary.Models;


namespace ProjectPropertiesWeb.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    [BindProperty]
    public required Details Details { get; set; }
    private readonly ILogger<IndexModel> _logger = logger;

    public void OnGet()
    {
        Details = GetAllInfo();
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
