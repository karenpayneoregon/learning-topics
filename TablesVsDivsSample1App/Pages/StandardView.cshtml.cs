using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Data;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Pages;

public class StandardViewModel(Context context) : PageModel
{
    public IList<Customers> Customers { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Customers = await context.Customers.ToListAsync();
    }
}
