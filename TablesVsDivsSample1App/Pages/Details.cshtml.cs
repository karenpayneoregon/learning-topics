using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Data;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Pages;

public class DetailsModel(Context context) : PageModel
{
    public Customers Customers { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var customers = await context.Customers.FirstOrDefaultAsync(m => m.Id == id);
        if (customers is null)
        {
            return NotFound();
        }
        else
        {
            Customers = customers;
        }

        return Page();
    }
}
