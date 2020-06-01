using System;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesDeliveryEventFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var deliveryEvent = TestHelpers.GetSesDeliveryEventModel(dt);

            var sesDeliveryEvent = deliveryEvent.Create(1);

            Assert.Equal(sesDeliveryEvent.Recipients, string.Join(',', deliveryEvent.Delivery.Recipients));
            Assert.Equal(sesDeliveryEvent.SmtpResponse, deliveryEvent.Delivery.SmtpResponse);
            Assert.Equal(sesDeliveryEvent.DeliveredAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesDeliveryEvent.SentAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesDeliveryEvent.SendingAccountId, deliveryEvent.Mail.SendingAccountId);
            Assert.Equal(sesDeliveryEvent.ReportingMta, deliveryEvent.Delivery.ReportingMta);
            Assert.Equal(sesDeliveryEvent.SourceArn, deliveryEvent.Mail.SourceArn);
            Assert.Equal(sesDeliveryEvent.Source, deliveryEvent.Mail.Source);
            Assert.Equal(sesDeliveryEvent.SourceIp, deliveryEvent.Mail.SourceIp);
            Assert.Equal(sesDeliveryEvent.NotificationType, deliveryEvent.EventType);
            Assert.Equal(1, sesDeliveryEvent.NotificationId);
            Assert.Equal(sesDeliveryEvent.MessageId, deliveryEvent.Mail.MessageId);
        }
    }
}
