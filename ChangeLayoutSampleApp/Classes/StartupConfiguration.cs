namespace ChangeLayoutSampleApp.Classes;

public class StartupConfiguration
{
    /// <summary>
    /// Gets the layout name based on the current day.
    /// </summary>
    /// <value>
    /// Returns "_Layout" if the current day is a weekday; otherwise, returns "_LayoutWeekend".
    /// </value>
    public static string Decide 
        => DateTime.Now.IsWeekDay() ? 
            "_Layout" : 
            "_LayoutWeekend";
}