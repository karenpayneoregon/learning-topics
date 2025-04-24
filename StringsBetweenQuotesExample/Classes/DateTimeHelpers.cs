namespace StringsBetweenQuotesExample.Classes;

public static class DateTimeHelpers
{
    /// <summary>
    /// Generates a list of dates representing the next week's dates starting from the upcoming Sunday.
    /// </summary>
    /// <returns>A list of <see cref="DateOnly"/> objects representing the dates of the next week.</returns>
    public static List<DateOnly> NextWeeksDates()
    {
        var start = DateTime.Now;
        var nextSunday = DateOnly.FromDateTime(start).Next(DayOfWeek.Sunday);
        return Enumerable.Range(0, 7)
            .Select(index => nextSunday.AddDays(index))
            .ToList();
    }

    /// <summary>
    /// Generates a list of all days in the specified month of the current year.
    /// </summary>
    /// <param name="month">The month for which to generate the list of days (1 to 12).</param>
    /// <returns>A list of <see cref="DateOnly"/> objects representing each day in the specified month.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="month"/> is not in the range 1 to 12.
    /// </exception>
    public static List<DateOnly> GetMonthDays(int month)
    {
        int year = DateTime.Now.Year;

        return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
            .Select(day => new DateOnly(year, month, day))
            .ToList();
    }

    /// <summary>
    /// Divides the days of a specified month and year into weeks, where each week is represented as a list of days.
    /// </summary>
    /// <param name="year">The year of the month to process.</param>
    /// <param name="month">The month to process (1 to 12).</param>
    /// <returns>A list of weeks, where each week is a list of <see cref="DateOnly"/> objects representing the days in that week.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the <paramref name="month"/> is not in the range 1 to 12.
    /// </exception>
    public static List<List<DateOnly>> GetWeeksInMonth(int year, int month)
    {
        List<List<DateOnly>> weeks = [];
        DateOnly firstDay = new(year, month, 1);
        DateOnly lastDay = new(year, month, DateTime.DaysInMonth(year, month));

        List<DateOnly> currentWeek = [];

        for (var day = firstDay; day <= lastDay; day = day.AddDays(1))
        {
            if (day.DayOfWeek == DayOfWeek.Sunday && currentWeek.Count > 0)
            {
                weeks.Add(currentWeek);
                currentWeek = [];
            }

            currentWeek.Add(day);

            if (day == lastDay)
            {
                weeks.Add(currentWeek);
            }
        }

        return weeks;
    }

    /// <summary>
    /// Calculates the next occurrence of the specified day of the week, starting from the given date.
    /// </summary>
    /// <param name="from">The starting date to calculate from.</param>
    /// <param name="dayOfWeek">The target day of the week to find.</param>
    /// <returns>A <see cref="DateOnly"/> object representing the next occurrence of the specified day of the week.</returns>
    public static DateOnly Next(this DateOnly from, DayOfWeek dayOfWeek)
    {
        int start = (int)from.DayOfWeek;
        int target = (int)dayOfWeek;
        if (target <= start)
        {
            target += 7;
        }

        return from.AddDays(target - start);
    }
}