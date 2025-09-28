using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace MultipleSubmitButtons1.Pages;

/// <summary>
/// Represents the model for the Index1 Razor Page, handling multiple submit buttons functionality.
/// </summary>
/// <remarks>
/// This class is responsible for binding and validating user input, managing session state, 
/// and redirecting to another page upon successful form submission.
/// </remarks>
public class Index1Model(IOptions<Dictionary<string, string>> programs) : PageModel
{
    public Dictionary<string, string> Programs { get; } = programs.Value;

    // Bind the posted fields
    [BindProperty]
    public string? Program { get; set; }


    [BindProperty]
    [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
    public int Count { get; set; }

    /// <summary>
    /// Handles the form submission for the "Button1" action on the Index1 Razor Page.
    /// </summary>
    /// <remarks>
    /// This method validates the model state, processes the selected program and count values,
    /// stores them in the session, and redirects to the "Index2" page upon successful submission.
    /// </remarks>
    /// <returns>
    /// An <see cref="IActionResult"/> that either redisplay the page if the model state is invalid
    /// or redirects to the "Index2" page if the submission is successful.
    /// </returns>
    public IActionResult OnPostButton1()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Key is what got posted from the <select>
        var selectedKey = Program;

        // Lookup the value (friendly display text) from Programs dictionary
        Programs.TryGetValue(selectedKey!, out var selectedValue);
        Console.WriteLine(Program);

        HttpContext.Session.SetString("Program", selectedValue!);
        HttpContext.Session.SetInt32("Count", Count);

        return RedirectToPage("Index2");
        
    }
}