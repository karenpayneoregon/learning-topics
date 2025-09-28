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

    /// <summary>
    /// Gets the main count of sessions requested by the user.
    /// </summary>
    /// <remarks>
    /// This property is set during the form submission process in the <see cref="OnPost"/> method.
    /// It represents the number of sessions entered by the user in the input field.
    /// If the form submission is invalid or an unknown action is selected, this property will be set to <c>null</c>.
    /// </remarks>
    public int? MainCount { get; private set; }
    
    /// <summary>
    /// Gets the name of the selected program based on the user's action.
    /// </summary>
    /// <remarks>
    /// This property is set during the <see cref="OnPost"/> method when a valid action is submitted.
    /// If the action corresponds to a known program, this property will hold the program's name.
    /// Otherwise, it will remain <c>null</c>.
    /// </remarks>
    public string? Program { get; private set; }

    /// <summary>
    /// Gets the collection of available programs, where the key represents the program identifier
    /// and the value represents the program name.
    /// </summary>
    /// <remarks>
    /// The programs are populated from the application configuration (e.g., appsettings.json).
    /// </remarks>
    public Dictionary<string, string> Programs { get; } = programs.Value;

    public void OnGet()
    {
    }

    /// <summary>
    /// Handles the form submission for the Index page.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> that renders the page with updated state.
    /// </returns>
    /// <remarks>
    /// This method processes the user's input and determines the selected action.
    /// - If the form submission is invalid, the page is re-rendered with validation errors.
    /// - If the action corresponds to a known program, the <see cref="Program"/> property is updated with the program's name.
    /// - If the action is unknown, an error is added to the model state, and <see cref="MainCount"/> is set to <c>null</c>.
    /// * As coded in the example, this should not happen.
    /// </remarks>
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!Request.Form.TryGetValue("action", out var actionValue) || actionValue.Count == 0 || actionValue[0] is null)
        {
            ModelState.AddModelError(string.Empty, "Action is required.");
            MainCount = null;
            return Page();
        }

        MainCount = CountInput;

        var actionKey = actionValue[0]!;
        if (Programs.TryGetValue(actionKey, out var programName))
        {
            Program = programName;
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Unknown action.");
            MainCount = null;
        }

        return Page();
    }
}