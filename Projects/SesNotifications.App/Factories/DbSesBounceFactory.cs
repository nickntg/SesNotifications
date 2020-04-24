using System;
using System.Linq;
using SesNotifications.App.Models;
using SesBounce = SesNotifications.DataAccess.Entities.SesBounce;

namespace SesNotifications.App.Factories
{
    public static class DbSesBounceFactory
    {
        public static SesBounce Create(this SesBounceModel bounce, long notificationId)
        {
            return new SesBounce
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
                RemoteMtaIp = bounce.Bounce.RemoteMtaIp,
                BouncedRecipients = string.Join(',', bounce.Bounce.BouncedRecipients.Select(x => x.EmailAddress).ToArray())
            };
        }
    }
}
