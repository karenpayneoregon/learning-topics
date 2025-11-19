using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpersLibrary;
public static class DateTimeExtensions
{
    public static (DateOnly saturday, DateOnly sunday) GetWeekendDates(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
    {
        int diff = ((int)date.DayOfWeek - (int)startOfWeek + 7) % 7;
        var weekStart = date.AddDays(-diff);

        var saturday = weekStart.AddDays(6);  // 6 = Saturday offset when startOfWeek = Sunday
        var sunday = weekStart.AddDays(7);    // next day

        return (DateOnly.FromDateTime(saturday),
            DateOnly.FromDateTime(sunday));
    }

    public static (DateOnly saturday, DateOnly sunday) GetWeekendDatesUtc(this DateTime utcDate, DayOfWeek startOfWeek = DayOfWeek.Sunday)
    {
        if (utcDate.Kind != DateTimeKind.Utc)
            throw new ArgumentException("utcDate must be UTC", nameof(utcDate));

        return GetWeekendDates(utcDate, startOfWeek);
    }


    public static (DateOnly saturday, DateOnly sunday) GetPreviousWeekendDates(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        => GetWeekendDates(date.AddDays(-7), startOfWeek);

    public static (DateOnly saturday, DateOnly sunday) GetNextWeekendDates(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        => GetWeekendDates(date.AddDays(7), startOfWeek);


}