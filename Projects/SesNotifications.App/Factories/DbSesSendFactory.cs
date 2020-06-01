using System;
using SesNotifications.App.Models;
using SesSend = SesNotifications.DataAccess.Entities.SesSend;

namespace SesNotifications.App.Factories
{
    public static class DbSesSendFactory
    {
        public static SesSend Create(this SesSendModel open, long notificationId)
        {
            return new SesSend
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
