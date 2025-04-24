using NodaTime;
using static NodaTime.PeriodUnits;
using static NodaTime.SystemClock;


namespace StringsBetweenQuotesExample.Classes;

public class NodaHelpers
{
    /// <summary>
    /// Calculates the time difference between the current time in a specified time zone
    /// and a given future date and time.
    /// </summary>
    /// <param name="futureDateTime">The future date and time to compare against the current time.</param>
    /// <param name="timeZoneId">The identifier of the time zone to use for the calculation.</param>
    /// <returns>
    /// A string representation of the current time, the future time, and the difference
    /// between them in years, months, days, hours, minutes, and seconds.
    /// </returns>
    public static string GetTimeDifference(DateTime futureDateTime, string timeZoneId = "America/Los_Angeles")
    {
        DateTimeZone timeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZoneId);
        if (timeZone == null)
        {
            return $"Error: The time zone '{timeZoneId}' is invalid.";
        }

        ZonedDateTime now = Instance.GetCurrentInstant().InZone(timeZone);
        LocalDateTime futureLocal = LocalDateTime.FromDateTime(futureDateTime);
        ZonedDateTime future = futureLocal.InZoneLeniently(timeZone);

        Period difference = Period.Between(now.LocalDateTime, futureLocal, 
            Days | Hours | Minutes | Seconds);

        return $"Current Time: {now.ToString("MM/dd/yyyy hh:mm:ss", null)}" +
               $"\nFuture Time: {future.ToString("MM/dd/yyyy hh:mm:ss", null)}\n" +
               $"Difference: {difference.Years} years, {difference.Months} months, " +
               $"{difference.Days} days, {difference.Hours} hours, {difference.Minutes} minutes, " +
               $"{difference.Seconds} seconds.";
    }
}