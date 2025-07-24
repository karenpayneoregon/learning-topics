

using BogusLibrary1.Interfaces;
using BogusLibrary1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;


namespace BogusDateOnlyTimeOnlyApp.Pages;
public class IndexModel(IMockedData mockedData) : PageModel
{
    [BindProperty]
    public required List<Customer> CustomersRecent { get; set; }
    [BindProperty]
    public required List<Customer> CustomersSoon { get; set; }
    [BindProperty]
    public required List<Customer> CustomersBetween { get; set; }

    public void OnGet()
    {
        CustomersRecent = mockedData.GetCustomersDateOnlyTimeOnlyRecent(5);
        Log.Information("\n{@L1}", CustomersRecent);

        CustomersSoon = mockedData.GetCustomersTimeOnlySoon(5);

        CustomersBetween = mockedData.GetCustomersBetweenTimeOnlyAndDateOnly(5);
    }
}
