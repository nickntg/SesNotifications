using System;
using SesNotifications.App.Models;
using SesNotifications.DataAccess.Entities;
using SesSendEvent = SesNotifications.DataAccess.Entities.SesSendEvent;

namespace SesNotifications.App.Factories
{
    public static class DbSesSendEventFactory
    {
        public static SesSendEvent Create(this SesSendEventModel open, long notificationId)
        {
            return new SesSendEvent
            {
                Id = notificationId,
                NotificationId = notificationId,
                NotificationType = "Send",
                SentAt = Convert.ToDateTime(open.Mail.Timestamp),
                MessageId = open.Mail.MessageId,
                Source = open.Mail.Source,
                SourceArn = open.Mail.SourceArn,
                SourceIp = open.Mail.SourceIp,
                SendingAccountId = open.Mail.SendingAccountId,
                Recipients = string.Join(',', open.Mail.Destination)
            };
        }
    }
}
