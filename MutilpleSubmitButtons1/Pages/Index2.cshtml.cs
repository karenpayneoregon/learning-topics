#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MultipleSubmitButtons1.Pages;

public class Index2Model : PageModel
{
    public string ProgramParam { get; private set; }
    public int? CountParam { get; private set; }

    /// <summary>
    /// Handles GET requests to the page from Index1.
    /// </summary>
    /// <remarks>
    /// This method initializes the <see cref="ProgramParam"/> and <see cref="CountParam"/> properties
    /// by retrieving their values from the session state.
    /// </remarks>
    public void OnGet()
    {
        ProgramParam = HttpContext.Session.GetString("Program");
        CountParam = HttpContext.Session.GetInt32("Count");
    }
}