using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Data;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Pages
{
    public class UsingDivsPageModel(Context context) : PageModel
    {
        public IList<Customers> Customers { get; set; } = null!;

        public async Task OnGetAsync()
        {
            Customers = await context.Customers.ToListAsync();
        }

        /// <summary>
        /// Handles the HTTP POST request to delete a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the operation:
        /// <list type="bullet">
        /// <item><description><see cref="NotFoundResult"/> if the customer with the specified <paramref name="id"/> is not found.</description></item>
        /// <item><description><see cref="RedirectToPageResult"/> to redirect to the current page after successful deletion.</description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// This method retrieves the customer from the database using the provided <paramref name="id"/>.
        /// If the customer exists, it is removed from the database, and changes are saved.
        /// </remarks>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return RedirectToPage();
        }

    }
}
