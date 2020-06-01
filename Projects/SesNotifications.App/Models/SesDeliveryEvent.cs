namespace SesNotifications.App.Models
{
    public class SesDeliveryEvent
    {
        public virtual string Timestamp { get; set; }
        public virtual string[] Recipients { get; set; }
        public virtual string SmtpResponse { get; set; }
        public virtual string ReportingMta { get; set; }
    }
}