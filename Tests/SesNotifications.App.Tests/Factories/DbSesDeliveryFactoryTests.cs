using System;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesDeliveryFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var delivery = TestHelpers.GetSesDeliveryModel(dt);

            var sesDelivery = delivery.Create(1);

            Assert.Equal(sesDelivery.Recipients, string.Join(',', delivery.Delivery.Recipients));
            Assert.Equal(sesDelivery.SmtpResponse, delivery.Delivery.SmtpResponse);
            Assert.Equal(sesDelivery.DeliveredAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesDelivery.SentAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesDelivery.SendingAccountId, delivery.Mail.SendingAccountId);
            Assert.Equal(sesDelivery.ReportingMta, delivery.Delivery.ReportingMta);
            Assert.Equal(sesDelivery.SourceArn, delivery.Mail.SourceArn);
            Assert.Equal(sesDelivery.Source, delivery.Mail.Source);
            Assert.Equal(sesDelivery.SourceIp, delivery.Mail.SourceIp);
            Assert.Equal(sesDelivery.NotificationType, delivery.NotificationType);
            Assert.Equal(1, sesDelivery.NotificationId);
            Assert.Equal(sesDelivery.MessageId, delivery.Mail.MessageId);
        }
    }
}