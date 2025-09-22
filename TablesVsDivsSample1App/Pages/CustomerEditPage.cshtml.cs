using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Data;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Pages
{
    /// <summary>
    /// Represents the page model for editing customer information.
    /// </summary>
    /// <remarks>
    /// This class is used to handle the logic for retrieving and updating customer data
    /// within this page for editing customers. It interacts with the database context
    /// to perform operations such as fetching customer details and saving changes.
    /// </remarks>
    public class CustomerEditPageModel(Context context) : PageModel
    {
        
        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        [BindProperty]
        public Customers Customers { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            Customers = customers;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.Attach(Customers).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Customers.Any(e => e.Id == Customers.Id))
                {
                    return NotFound();
                }
            }

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return LocalRedirect(ReturnUrl);
            }

            // Fallback: go to whatever makes sense as a default
            return RedirectToPage("./Index");
        }
    }
}
