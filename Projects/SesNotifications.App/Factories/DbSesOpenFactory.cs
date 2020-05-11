using System;
using SesNotifications.App.Models;
using SesOpen = SesNotifications.DataAccess.Entities.SesOpen;

namespace SesNotifications.App.Factories
{
    public static class DbSesOpenFactory
    {
        public static SesOpen Create(this SesOpenModel open, long notificationId)
        {
            return new SesOpen
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