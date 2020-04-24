namespace SesNotifications.App.Models
{
    public class SesDelivery
    {
        public virtual string Timestamp { get; set; }
        public virtual string[] Recipients { get; set; }
        public virtual string SmtpResponse { get; set; }
        public virtual string ReportingMta { get; set; }
        public virtual string RemoteMtaIp { get; set; }
    }
}