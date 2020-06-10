namespace SesNotifications.App.Helpers
{
    public static class StringHelpers
    {
        public static string PrepareForLike(this string email)
        {
            return string.IsNullOrEmpty(email) ? email : $"%{email}%";
        }
    }
}