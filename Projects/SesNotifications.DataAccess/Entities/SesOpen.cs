using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesOpen : SesCommon
    {
        public virtual string Recipients { get; set; }
        public virtual DateTime OpenedAt { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string IpAddress { get; set; }
    }
}
