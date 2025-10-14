namespace WindowsTimeApp.Classes;
internal class MainOperations
{
    /// <summary>
    /// Displays the system uptime information in a formatted output.
    /// </summary>
    /// <remarks>
    /// This method retrieves the system uptime details and outputs them using a formatted display.
    /// It internally calls the <see cref="WindowsTimeApp.Classes.WindowsCode.Show"/> method.
    /// </remarks>
    public static void ShowTime()
    {
        WindowsCode.Show();
    }
}
