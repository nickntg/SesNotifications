namespace SesNotifications.App.Models
{
    public class SesOpenEvent
    {
        public virtual string Timestamp { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string IpAddress { get; set; }
    }
}