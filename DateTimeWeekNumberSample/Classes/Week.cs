using System.ComponentModel;


namespace DateTimeWeekNumberSample.Classes;

/// <summary>
/// Represents a specific week in a particular year.
/// </summary>
/// <remarks>
/// This class provides properties to define a week using its year and week number. 
/// It also includes functionality for parsing and formatting week representations.
/// </remarks>
[TypeConverter(typeof(WeekConverter))]
public class Week
{
    public int Year { get; set; }
    public int WeekNumber { get; set; }

    public static Week TryParse(string input)
    {

        var result = input.Split("-W");
        
        if (result.Length != 2)
        {
            return null;
        }

        if (int.TryParse(result[0], out var year) && int.TryParse(result[1], out var week))
        {
            return new Week { Year = year, WeekNumber = week };
        }

        return null;
    }

    public override string ToString() 
        => $"{Year}-W{WeekNumber:D2}";
}