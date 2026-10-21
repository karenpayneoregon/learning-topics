namespace EnvironmentApplication.Classes
{
    public static class DateHelpers
    {
        public static bool IsNotWeekend(this DateTime senderDate)
        {
            DateTime date = Convert.ToDateTime(senderDate);
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
