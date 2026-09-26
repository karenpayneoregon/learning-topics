using static System.Globalization.DateTimeFormatInfo;
using SingletonWebApp.Models;

namespace SingletonWebApp.Classes;

public sealed class Helpers
{
    private static readonly Lazy<Helpers> Lazy = new(() => new Helpers());
    public static Helpers Instance => Lazy.Value;

    public List<MonthItem> MonthList =>
    [
        .. CurrentInfo.MonthNames[..^1]
            .Select((monthName, index) => 
                new MonthItem(index + 1, monthName))
    ];
}