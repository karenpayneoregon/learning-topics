using System.Text.Json;


namespace WindowsTimeApp.Classes;

/// <summary>
/// Provides methods and data structures for retrieving and processing system uptime information.
/// </summary>
public class WindowsCode
{
    /// <summary>
    /// Represents the system uptime details, including weeks, days, hours, minutes, seconds,
    /// total days, and the system boot time.
    /// </summary>
    public class SystemUptimeDto
    {
        public int Weeks { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        /// <summary>Total uptime in days (fractional).</summary>
        public double TotalDays { get; set; }

        /// <summary>System boot time (local time).</summary>
        public DateTime BootTime { get; set; }

        public override string ToString() =>
            $"Uptime: {Weeks} week(s), {Days} day(s), {Hours} hour(s), {Minutes} minute(s), {Seconds} second(s)\n" +
            $"Total Days: {TotalDays:F2}\n" +
            $"Boot Time: {BootTime:O}";
    }

    /// <summary>
    /// Retrieves the system uptime information, including details such as weeks, days, hours, minutes, seconds,
    /// total days, and the system boot time.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="WindowsTimeApp.Classes.WindowsCode.SystemUptimeDto"/> containing the system uptime details.
    /// </returns>
    public static SystemUptimeDto GetSystemUptime()
    {
        var uptimeMs = Environment.TickCount64;
        var uptime = TimeSpan.FromMilliseconds(uptimeMs);

        var weeks = uptime.Days / 7;
        var days = uptime.Days % 7;

        // Compute boot time in UTC to avoid DST jumps, then convert to local
        var bootUtc = DateTime.UtcNow - uptime;
        var bootLocal = bootUtc.ToLocalTime();

        return new SystemUptimeDto
        {
            Weeks = weeks,
            Days = days,
            Hours = uptime.Hours,
            Minutes = uptime.Minutes,
            Seconds = uptime.Seconds,
            TotalDays = uptime.TotalDays,
            BootTime = bootLocal
        };
    }

    /// <summary>
    /// Retrieves the system uptime information and serializes it into a JSON string.
    /// </summary>
    /// <param name="indented">
    /// A boolean value indicating whether the JSON output should be formatted with indentation.
    /// If <c>true</c>, the JSON output will be indented for readability; otherwise, it will be compact.
    /// </param>
    /// <returns>
    /// A JSON string representing the system uptime information, including details such as weeks, days, hours, minutes, seconds,
    /// total days, and the system boot time.
    /// </returns>
    public static string GetSystemUptimeAsJson(bool indented = true)
    {
        var dto = GetSystemUptime();

        return JsonSerializer.Serialize(dto, JsonSerializerOptions);
    }

    private static JsonSerializerOptions JsonSerializerOptions => new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
}