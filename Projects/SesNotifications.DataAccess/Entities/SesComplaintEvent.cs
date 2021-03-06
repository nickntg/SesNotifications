﻿using System;

namespace SesNotifications.DataAccess.Entities
{
    public class SesComplaintEvent : SesCommon
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual string ComplaintSubType { get; set; }
        public virtual string ComplaintFeedbackType { get; set; }
        public virtual string FeedbackId { get; set; }
        public virtual string ComplainedRecipients { get; set; }
        public virtual DateTime? ArrivalDate { get; set; }
    }
}