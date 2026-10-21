using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Models;
using TablesVsDivsSample1App.Data;

namespace TablesVsDivsSample1App.Pages.CustomersPages;

public class DetailsModel : PageModel
{
    private readonly Context _context;
    public DetailsModel(Context context)
    {
        _context = context;
    }

    public Customers Customers { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var customers = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
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
