namespace SesNotifications.App.Models
{
    public class SesBounceEvent
    {
        public virtual string BounceType { get; set; }
        public virtual string BounceSubType { get; set; }
        public virtual SesBouncedRecipient[] BouncedRecipients { get; set; }
        public virtual string Timestamp { get; set; }
        public virtual string FeedbackId { get; set; }
        public virtual string ReportingMta { get; set; }
    }
}