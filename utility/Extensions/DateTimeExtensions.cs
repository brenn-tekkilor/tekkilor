using System;

namespace Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime NextWeekday(
            this DateTime caller, DayOfWeek day, bool includeCaller)
        {
            caller = includeCaller ? caller : caller.AddDays(1);
            return caller.AddDays(
                ((int)day - (int)caller.DayOfWeek + 7) % 7);
        }
        public static DateTime ToNextWed(
            this DateTime caller)
        {
            return caller.NextWeekday(DayOfWeek.Wednesday, false);
        }
        public static DateTime ToDateIfLater(
            this DateTime caller, DateTime toCompare)
        {
             return caller > toCompare ? caller : toCompare;
        }
        public static DateTime ToNextWeekdayIfLater(
            this DateTime caller, DayOfWeek day, bool includeToday)
        {
            DateTime nextWeekday = DateTime.Today.NextWeekday(day, includeToday);
            return caller > nextWeekday ? caller : nextWeekday;
        }
        public static DateTime ToNextWedIfLater(this DateTime caller)
        {
            return caller.ToNextWeekdayIfLater(DayOfWeek.Wednesday, false);
        }
    }
}
