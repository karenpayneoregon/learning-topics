namespace ChangeLayoutSampleApp.Classes;

public static class DateHelpers
{
    

    extension(DateTime self)
    {
        public bool IsWeekend()
            => self.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;

        public bool IsWeekDay()
            => !self.IsWeekend();
    }

    extension(DayOfWeek self)
    {
        public bool IsWeekDay()
        {
            return self is DayOfWeek.Monday or DayOfWeek.Tuesday or
                DayOfWeek.Wednesday or DayOfWeek.Thursday or DayOfWeek.Friday;
        }
    }

    extension(DayOfWeek self)
    {
        public bool IsWeekend()
        {
            return !self.IsWeekDay();
        }
    }
}