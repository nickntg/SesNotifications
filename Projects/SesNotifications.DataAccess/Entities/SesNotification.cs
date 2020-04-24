using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesNotification
    {
        public virtual long Id { get; set; }
        public virtual string Notification { get; set; }
        public virtual DateTime ReceivedAt { get; set; }
        public virtual DateTime SentAt { get; set; }
        public virtual string MessageId { get; set; }
    }
}
