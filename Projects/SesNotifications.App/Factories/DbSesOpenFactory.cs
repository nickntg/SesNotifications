using System;
using SesNotifications.App.Models;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Factories
{
    public static class DbSesOpenFactory
    {
        public static SesOpenEvent Create(this SesOpenModel open, long notificationId)
        {
            return new SesOpenEvent
            {
                Id = notificationId,
                NotificationId = notificationId,
                NotificationType = "Open",
                SentAt = Convert.ToDateTime(open.Mail.Timestamp),
                MessageId = open.Mail.MessageId,
                Source = open.Mail.Source,
                SourceArn = open.Mail.SourceArn,
                SourceIp = open.Mail.SourceIp,
                SendingAccountId = open.Mail.SendingAccountId,
                Recipients = string.Join(',', open.Mail.Destination),
                OpenedAt = Convert.ToDateTime(open.Open.Timestamp),
                UserAgent = open.Open.UserAgent,
                IpAddress = open.Open.IpAddress
            };
        }
    }
}