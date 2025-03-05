using CustomIConfigurationSourceRazorPages.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomIConfigurationSourceRazorPages.Classes;

public class DataOperations
{
    public static HelpDesk ReadFromDatabase(Context context)
    {
        List<Setting> settings = context.Settings.AsNoTracking().Where(x => x.Section == nameof(HelpDesk)).ToList();
        
        HelpDesk helpDesk = new()
        {
            Phone = settings.FirstOrDefault(x => x.Key == nameof(HelpDesk.Phone))?.Value,
            Email = settings.FirstOrDefault(x => x.Key == nameof(HelpDesk.Email))?.Value
        };

        return helpDesk;
    }
}
