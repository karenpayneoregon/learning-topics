using CustomIConfigurationSourceRazorPages.Models;
using CustomIConfigurationSourceSample.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomIConfigurationSourceRazorPages.Classes;

public class DataOperations
{
    /// <summary>
    /// Retrieves a <see cref="HelpDesk"/> object populated with phone and email settings
    /// from the database using the provided <paramref name="context"/>.
    /// </summary>
    /// <param name="context">
    /// The database context used to query the settings.
    /// </param>
    /// <returns>
    /// A <see cref="HelpDesk"/> object containing the phone and email values
    /// retrieved from the database. If no values are found, the properties will be null.
    /// </returns>
    public static HelpDesk ReadFromDatabase(Context context)
    {
        var settings = context.Settings
            .AsNoTracking()
            .Where(x => x.Section == nameof(HelpDesk) && (x.Key == nameof(HelpDesk.Phone) || x.Key == nameof(HelpDesk.Email)))
            .ToList();

        return new()
        {
            Phone = settings.FirstOrDefault(x => x.Key == nameof(HelpDesk.Phone))?.Value,
            Email = settings.FirstOrDefault(x => x.Key == nameof(HelpDesk.Email))?.Value
        };
    }

}
