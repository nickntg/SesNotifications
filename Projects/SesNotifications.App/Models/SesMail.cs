namespace SesNotifications.App.Models
{
    public class SesMail
    {
        public virtual string Timestamp { get; set; }
        public virtual string MessageId { get; set; }
        public virtual string Source { get; set; }
        public virtual string SourceArn { get; set; }
        public virtual string SourceIp { get; set; }
        public virtual string SendingAccountId { get; set; }
        public virtual string[] Destination { get; set; }
    }
}