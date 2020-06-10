using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesOperational
    {
        public virtual long NotificationId { get; set; }
        public virtual string NotificationType { get; set; }
        public virtual DateTime SentAt { get; set; }
        public virtual string Source { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string Recipients { get; set; }
        public virtual string Detail1 { get; set; }
        public virtual string Detail2 { get; set; }
    }
}