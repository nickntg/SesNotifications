using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesBounce : SesCommon
    {
        public virtual string BounceType { get; set; }
        public virtual string BounceSubType { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string FeedbackId { get; set; }
        public virtual string ReportingMta { get; set; }
        public virtual string RemoteMtaIp { get; set; }
        public virtual string BouncedRecipients { get; set; }
    }
}
