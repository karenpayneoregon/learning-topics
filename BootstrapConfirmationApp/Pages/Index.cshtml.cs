using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace BootstrapConfirmationApp.Pages;
public class IndexModel : PageModel
{

    public void OnGet()
    {

    }

    /// <summary>
    /// Handles the POST request for confirming an action.
    /// </summary>
    /// <param name="actionName">The name of the action to be confirmed.</param>
    /// <param name="itemId">The identifier of the item associated with the action.</param>
    /// <param name="answer">A boolean value indicating whether the action is confirmed (true) or canceled (false).</param>
    /// <returns>An <see cref="IActionResult"/> that redirects to the current page.</returns>
    public IActionResult OnPostConfirm(string actionName, int itemId, bool answer)
    {

        if (answer)
        {
            Log.Information("Continue {A}", actionName);
        }
        else
        {
            Log.Information("Cancel");
        }

        return RedirectToPage();
    }
}
