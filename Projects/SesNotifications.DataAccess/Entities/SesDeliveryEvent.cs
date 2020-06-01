using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesDeliveryEvent : SesCommon
    {
        public virtual DateTime DeliveredAt { get; set; }
        public virtual string SmtpResponse { get; set; }
        public virtual string ReportingMta { get; set; }
        public virtual string Recipients { get; set; }
    }
}
