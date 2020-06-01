using System;
using SesNotifications.App.Models;
using SesDeliveryEvent = SesNotifications.DataAccess.Entities.SesDeliveryEvent;

namespace SesNotifications.App.Factories
{
    public static class DbSesDeliveryEventFactory
    {
        public static SesDeliveryEvent Create(this SesDeliveryEventModel delivery, long notificationId)
        {
            return new SesDeliveryEvent
            {
                NotificationId = notificationId,
                NotificationType = "Delivery",
                SentAt = Convert.ToDateTime(delivery.Mail.Timestamp),
                MessageId = delivery.Mail.MessageId,
                Source = delivery.Mail.Source,
                SourceArn = delivery.Mail.SourceArn,
                SourceIp = delivery.Mail.SourceIp,
                SendingAccountId = delivery.Mail.SendingAccountId,
                DeliveredAt = Convert.ToDateTime(delivery.Delivery.Timestamp),
                SmtpResponse = delivery.Delivery.SmtpResponse,
                ReportingMta = delivery.Delivery.ReportingMta,
                Recipients = string.Join(',', delivery.Delivery.Recipients)
            };
        }
    }
}