using WineConsoleApp.Models;

namespace WineConsoleApp.Classes;

public sealed class AppData
{
    private static readonly Lazy<AppData> Lazy = new(() => new AppData());

    public static AppData Instance => Lazy.Value;

    /// <summary>
    /// Gets the collection of wines .
    /// </summary>
    /// <remarks>
    /// This property holds a list of <see cref="Models.Wine"/> objects, 
    /// representing the wines available in the application. The collection is initialized 
    /// using the <see cref="Classes.WineOperations.GetAllWines"/> method.
    /// </remarks>
    public List<Wine> Wines { get; set; }
    public string LogFileName => "Log.txt";

    private AppData()
    {
        Wines = WineOperations.GetAllWines(); ;
    }



}