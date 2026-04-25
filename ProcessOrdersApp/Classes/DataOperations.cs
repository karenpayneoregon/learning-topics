using ProcessOrdersApp.Models;
using System.Diagnostics;

namespace ProcessOrdersApp.Classes;
internal class DataOperations
{
    /// <summary>
    /// Processes a list of selected orders to a selected database.
    /// </summary>
    /// <param name="selected">A list of <see cref="Order"/> objects representing the selected orders to process.</param>
    /// <returns>
    /// A <see cref="bool"/> value indicating whether the processing was successful.
    /// </returns>

    public static bool ProcessOrders(List<Order> selected)
    {
        
        foreach (var (id, ordered, requiredBy, shipped, _, _, _, _, company) in selected)
        {
            Debug.WriteLine($"{id} {ordered} {requiredBy} {shipped} {company}");
        }
        
        return true;
        
    }
}
