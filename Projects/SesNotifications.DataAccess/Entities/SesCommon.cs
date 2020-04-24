using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesCommon
    {
        public virtual long Id { get; set; }
        public virtual long NotificationId { get; set; }
        public virtual string NotificationType { get; set; }
        public virtual DateTime SentAt { get; set; }
        public virtual string MessageId { get; set; }
        public virtual string Source { get; set; }
        public virtual string SourceArn { get; set; }
        public virtual string SourceIp { get; set; }
        public virtual string SendingAccountId { get; set; }
    }
}
