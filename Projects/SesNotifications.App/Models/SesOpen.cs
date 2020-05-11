namespace SesNotifications.App.Models
{
    public class SesOpen
    {
        public virtual string Timestamp { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string IpAddress { get; set; }
    }
}