﻿namespace SesNotifications.App.Models
{
    public class SesComplaintEvent
    {
        public virtual SesComplaintRecipient[] ComplainedRecipients { get; set; }
        public virtual string Timestamp { get; set; }
        public virtual string FeedbackId { get; set; }
        public virtual string ComplaintSubType { get; set; }
        public virtual string ComplaintFeedbackType { get; set; }
        public virtual string ArrivalDate { get; set; }
    }
}