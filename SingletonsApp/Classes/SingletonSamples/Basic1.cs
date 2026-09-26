using SingletonsApp.Models;
using static System.Globalization.DateTimeFormatInfo;

namespace SingletonsApp.Classes.SingletonSamples;

public sealed class Basic1
{
    private static readonly Lazy<Basic1> Lazy = new(() => new Basic1());
    public static Basic1 Instance => Lazy.Value;

    public List<MonthItem> MonthList =>
    [
        .. CurrentInfo.MonthNames[..^1]
            .Select((monthName, index) => 
                new MonthItem(index + 1, monthName))
    ];
}