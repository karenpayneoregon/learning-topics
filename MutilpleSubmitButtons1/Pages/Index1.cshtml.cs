using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace MultipleSubmitButtons1.Pages;

public class Index1Model(IOptions<Dictionary<string, string>> programs) : PageModel
{
    public Dictionary<string, string> Programs { get; } = programs.Value;

    // Bind the posted fields
    [BindProperty]
    public string? Program { get; set; }


    [BindProperty]
    [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
    public int Count { get; set; }


public IActionResult OnPostButton1()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Basic sanity check: force a selection
        if (string.IsNullOrWhiteSpace(Program))
        {
            ModelState.AddModelError(nameof(Program), "Please select a program.");
            return Page();
        }

        HttpContext.Session.SetString("Program", Program); 
        HttpContext.Session.SetInt32("Count", Count); 
        return RedirectToPage("Index2");
    }
}