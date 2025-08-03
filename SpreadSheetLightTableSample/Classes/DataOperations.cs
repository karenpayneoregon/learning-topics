using Microsoft.EntityFrameworkCore;
using Serilog;
using SpreadSheetLightTableSample.Data;
using SpreadSheetLightTableSample.Models;

namespace SpreadSheetLightTableSample.Classes;
public static class DataOperations
{
    public static async Task<(List<CustomerReportView> Data, bool IsSuccess)> GetCustomerReportData()
    {
        try
        {
            await using var context = new Context();
            var data = await context.Customers
                .Include(c => c.ContactTypeIdentifierNavigation)
                .Include(c => c.GenderIdentifierNavigation)
                .Select(c => new CustomerReportView
                {
                    Identifier = c.Identifier,
                    ContactFirstName = c.ContactFirstName,
                    ContactLastName = c.ContactLastName,
                    GenderType = c.GenderIdentifierNavigation.GenderType,
                    ContactType = c.ContactTypeIdentifierNavigation.ContactType
                })
                .ToListAsync();

            return (data, true);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error fetching customer report data");
            return ([], false);
        }
    }
}