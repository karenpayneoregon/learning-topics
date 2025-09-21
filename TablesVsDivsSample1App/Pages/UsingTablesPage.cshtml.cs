using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TablesVsDivsSample1App.Data;
using TablesVsDivsSample1App.Models;

namespace TablesVsDivsSample1App.Pages
{
    public class UsingTablesPageModel(Context context) : PageModel
    {
        public IList<Customers> Customers { get;set; } = null!;

        public async Task OnGetAsync()
        {
            Customers = await context.Customers.ToListAsync();
        }
    }
}
