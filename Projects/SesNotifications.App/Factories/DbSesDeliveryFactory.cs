using System;
using SesNotifications.App.Models;
using SesDelivery = SesNotifications.DataAccess.Entities.SesDelivery;

namespace SesNotifications.App.Factories
{
    public static class DbSesDeliveryFactory
    {
        public static SesDelivery Create(this SesDeliveryModel delivery, long notificationId)
        {
            return new SesDelivery
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
                RemoteMtaIp = delivery.Delivery.RemoteMtaIp,
                Recipients = string.Join(',', delivery.Delivery.Recipients)
            };
        }
    }
}
