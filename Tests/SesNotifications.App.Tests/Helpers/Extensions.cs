using System;

namespace SesNotifications.App.Tests.Helpers
{
    public static class Extensions
    {
        public static string Iso8601(this DateTime? dt)
        {
            return dt?.Iso8601();
        }

        public static string Iso8601(this DateTime dt)
        {
            return dt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}