using System;
using System.Linq;
using SesNotifications.App.Models;
using SesComplaintEvent = SesNotifications.DataAccess.Entities.SesComplaintEvent;

namespace SesNotifications.App.Factories
{
    public static class DbSesComplaintEventFactory
    {
        public static SesComplaintEvent Create(this SesComplaintEventModel complaint, long notificationId)
        {
            return new SesComplaintEvent
            {
                Id = notificationId,
                NotificationId = notificationId,
                NotificationType = "Complaint",
                SentAt = Convert.ToDateTime(complaint.Mail.Timestamp),
                MessageId = complaint.Mail.MessageId,
                Source = complaint.Mail.Source,
                SourceArn = complaint.Mail.SourceArn,
                SourceIp = complaint.Mail.SourceIp,
                SendingAccountId = complaint.Mail.SendingAccountId,
                CreatedAt = Convert.ToDateTime(complaint.Complaint.Timestamp),
                ComplaintSubType = complaint.Complaint.ComplaintSubType,
                ComplaintFeedbackType = complaint.Complaint.ComplaintFeedbackType,
                FeedbackId = complaint.Complaint.FeedbackId,
                ComplainedRecipients = string.Join(',', complaint.Complaint.ComplainedRecipients.Select(x => x.EmailAddress).ToArray()),
                ArrivalDate = !string.IsNullOrEmpty(complaint.Complaint.ArrivalDate) ? Convert.ToDateTime(complaint.Complaint.ArrivalDate) : (DateTime?)null
            };
        }
    }
}