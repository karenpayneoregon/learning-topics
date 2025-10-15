using SelectWithChecksSample.Models;
using System.Globalization;
using static System.Globalization.CultureInfo;

namespace SelectWithChecksSample.Classes;
public static class Lookups
{
    /// <summary>
    /// Generates a list of months as <see cref="CheckboxItem"/> objects, using the specified culture's month names.
    /// </summary>
    /// <param name="culture">
    /// The culture to use for retrieving month names. If <c>null</c>, the current culture (<see cref="CurrentCulture"/>) is used.
    /// </param>
    /// <returns>
    /// A list of <see cref="CheckboxItem"/> objects, each representing a month with its ID, name, and selection status.
    /// </returns>
    /// <remarks>
    /// If the specified culture's month names are unavailable or invalid, the invariant culture's month names are used as a fallback.
    ///
    /// BuildMonths()
    /// BuildMonths(new CultureInfo("fr-FR"))
    /// 
    /// </remarks>
    public static List<CheckboxItem> BuildMonths(CultureInfo culture = null)
    {
        culture ??= CurrentCulture;
        var dtf = culture.DateTimeFormat;

        var items = new List<CheckboxItem>(capacity: 12);
        for (int id = 1; id <= 12; id++)
        {
            var name = dtf.GetMonthName(id);
            if (string.IsNullOrWhiteSpace(name))
                name = InvariantCulture.DateTimeFormat.GetMonthName(id);

            items.Add(new CheckboxItem
            {
                Id = id,
                Text = name,
                IsSelected = false
            });
        }

        return items;
    }
}