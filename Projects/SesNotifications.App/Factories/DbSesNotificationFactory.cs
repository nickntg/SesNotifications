using System;
using SesNotifications.App.Models;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Factories
{
    public static class DbSesNotificationFactory
    {
        public static SesNotification Create(this SesMail mail, string content)
        {
            return new SesNotification
            {
                ReceivedAt = DateTime.UtcNow,
                MessageId = mail.MessageId,
                SentAt = Convert.ToDateTime(mail.Timestamp),
                Notification = content
            };
        }
    }
}