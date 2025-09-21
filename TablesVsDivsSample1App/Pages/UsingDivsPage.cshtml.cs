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
    }
}
