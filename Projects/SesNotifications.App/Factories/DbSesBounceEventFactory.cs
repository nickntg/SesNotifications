using SesNotifications.App.Models;
using System;
using System.Linq;
using SesBounceEvent = SesNotifications.DataAccess.Entities.SesBounceEvent;

namespace SesNotifications.App.Factories
{
    public static class DbSesBounceEventFactory
    {
        public static SesBounceEvent Create(this SesBounceEventModel bounce, long notificationId)
        {
            return new SesBounceEvent
            {
                Id = notificationId,
                NotificationId = notificationId,
                NotificationType = "Bounce",
                SentAt = Convert.ToDateTime(bounce.Mail.Timestamp),
                MessageId = bounce.Mail.MessageId,
                Source = bounce.Mail.Source,
                SourceArn = bounce.Mail.SourceArn,
                SourceIp = bounce.Mail.SourceIp,
                SendingAccountId = bounce.Mail.SendingAccountId,
                BounceType = bounce.Bounce.BounceType,
                BounceSubType = bounce.Bounce.BounceSubType,
                CreatedAt = Convert.ToDateTime(bounce.Bounce.Timestamp),
                FeedbackId = bounce.Bounce.FeedbackId,
                ReportingMta = bounce.Bounce.ReportingMta,
                BouncedRecipients = string.Join(',', bounce.Bounce.BouncedRecipients.Select(x => x.EmailAddress).ToArray())
            };
        }
    }
}