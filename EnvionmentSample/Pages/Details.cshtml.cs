using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace EnvironmentApplication.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly OedContext _context;
        public Taxpayer? Taxpayer { get; set; }
        public DetailsModel(OedContext context)
        {
            _context = context;
        }
        public async Task OnGet(int? id)
        {
            Taxpayer = await _context.Taxpayer
                .Include(tp => tp.Category)
                .FirstOrDefaultAsync(tp => tp.Id == id);
        }
    }
}
