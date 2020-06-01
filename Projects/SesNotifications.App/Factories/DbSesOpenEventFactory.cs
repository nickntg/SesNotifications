using System;
using SesNotifications.App.Models;
using SesNotifications.DataAccess.Entities;
using SesOpenEvent = SesNotifications.DataAccess.Entities.SesOpenEvent;

namespace SesNotifications.App.Factories
{
    public static class DbSesOpenEventFactory
    {
        public static SesOpenEvent Create(this SesOpenEventModel openEvent, long notificationId)
        {
            return new SesOpenEvent
            {
                Id = notificationId,
                NotificationId = notificationId,
                NotificationType = "Open",
                SentAt = Convert.ToDateTime(openEvent.Mail.Timestamp),
                MessageId = openEvent.Mail.MessageId,
                Source = openEvent.Mail.Source,
                SourceArn = openEvent.Mail.SourceArn,
                SourceIp = openEvent.Mail.SourceIp,
                SendingAccountId = openEvent.Mail.SendingAccountId,
                Recipients = string.Join(',', openEvent.Mail.Destination),
                OpenedAt = Convert.ToDateTime(openEvent.Open.Timestamp),
                UserAgent = openEvent.Open.UserAgent,
                IpAddress = openEvent.Open.IpAddress
            };
        }
    }
}