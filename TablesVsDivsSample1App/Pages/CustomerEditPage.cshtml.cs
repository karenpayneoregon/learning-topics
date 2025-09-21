using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Data;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Pages
{
    public class CustomerEditPageModel(Context context) : PageModel
    {
        private readonly Context _context = context;

        // Bind on GET and POST so the hidden field round-trips on form submit
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

            var customers = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
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

            _context.Attach(Customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(e => e.Id == Customers.Id))
                {
                    return NotFound();
                }
            }

            // Only redirect to local URLs to avoid open redirects
            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                return LocalRedirect(ReturnUrl);

            // Fallback: go to whatever makes sense as a default
            return RedirectToPage("./Index");
        }
    }
}
