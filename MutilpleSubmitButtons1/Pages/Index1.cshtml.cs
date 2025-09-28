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

        // Key is what got posted from the <select>
        var selectedKey = Program;

        // Lookup the value (friendly display text) from Programs dictionary
        Programs.TryGetValue(selectedKey, out var selectedValue);


        HttpContext.Session.SetString("Program", Program); 
        HttpContext.Session.SetInt32("Count", Count); 
        return RedirectToPage("Index2");
    }
}