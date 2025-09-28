using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MultipleSubmitButtons1.Pages;

public class Index1Model(IOptions<Dictionary<string, string>> programs) : PageModel
{

    public Dictionary<string, string> Programs { get; } = programs.Value;

    /// <summary>
    /// Handles the POST request triggered by the "Button1" action.
    /// </summary>
    /// <remarks>
    /// This method sets session values for "Program" and "Count" and redirects to the "Index2" page.
    /// The "Program" session value is selected randomly from the dictionary of programs using 
    /// <see cref="Enumerable.OrderBy{TSource,TKey}(IEnumerable{TSource},Func{TSource,TKey})"/> 
    /// with a random GUID as the key to ensure randomness. Since there are only a few entries expect repeats.
    /// </remarks>
    /// <returns>
    /// An <see cref="IActionResult"/> that redirects to the "Index2" page.
    /// </returns>
    public IActionResult OnPostButton1()
    {
;
        HttpContext.Session.SetString("Program", Programs.Values.OrderBy(_ => Guid.NewGuid()).First());
        HttpContext.Session.SetInt32("Count", 10);
        return RedirectToPage("Index2");
    }
}