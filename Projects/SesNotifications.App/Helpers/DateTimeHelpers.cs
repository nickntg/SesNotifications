using System;

namespace SesNotifications.App.Helpers
{
    public static class DateTimeHelpers
    {
        public static DateTime StartOfDay (this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime EndOfDay (this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999, DateTimeKind.Utc);
        }
    }
}