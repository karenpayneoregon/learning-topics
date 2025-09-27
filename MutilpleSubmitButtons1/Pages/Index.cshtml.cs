using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MultipleSubmitButtons1.Pages;

/// <summary>
/// Represents the model for the Index page, handling session selection and program actions.
/// </summary>
/// <remarks>
/// - See appsettings.json for the list of programs.
/// - See Program.cs for service configuration for reading the programs from appsettings.json.
/// </remarks>
public class IndexModel(IOptions<Dictionary<string, string>> programs) : PageModel
{
    [BindProperty]
    [Range(1, int.MaxValue, ErrorMessage = "Enter at least 1.")]
    public int CountInput { get; set; }

    public int? SessionCount { get; private set; }
    public string? Program { get; private set; }

    public Dictionary<string, string> Programs { get; } = programs.Value;

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var currentAction = Request.Form["action"].ToString();
        SessionCount = CountInput;

        if (Programs.TryGetValue(currentAction, out var programName))
        {
            Program = programName;
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Unknown action.");
            SessionCount = null;
        }

        return Page();
    }
}