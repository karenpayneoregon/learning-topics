using EnvironmentApplication.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
#pragma warning disable CS8618

namespace EnvironmentApplication.Pages
{
    public class TaxpayersModel : PageModel
    {
        private readonly OedContext _context;

        [ViewData]
        public string Title { get; set; }

        public TaxpayersModel(OedContext context)
        {
            _context = context;

            IConfigurationRoot configuration = Configurations.GetConfigurationRoot();
            Title = configuration.GetSection("ApplicationSettings")["Title"] + " Taxpayers";
        }

        public IList<Taxpayer> Taxpayer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Taxpayer != null)
            {
                Taxpayer = await _context.Taxpayer
                .Include(t => t.Category)
                .ToListAsync();
            }
        }
    }
}
