using System;
using SesNotifications.App.Models;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Factories
{
    public static class DbSesSendFactory
    {
        public static SesSendEvent Create(this SesSendModel open, long notificationId)
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
