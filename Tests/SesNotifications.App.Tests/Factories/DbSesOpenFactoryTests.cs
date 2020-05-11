using System;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesOpenFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var open = TestHelpers.GetSesOpenModel(dt);

            var sesOpen = open.Create(1);

            Assert.Equal(sesOpen.Recipients, string.Join(',', open.Mail.Destination));
            Assert.Equal(sesOpen.OpenedAt.Iso8601(), open.Open.Timestamp);
            Assert.Equal(sesOpen.SourceArn, open.Mail.SourceArn);
            Assert.Equal(sesOpen.NotificationType, open.NotificationType);
            Assert.Equal(1, sesOpen.NotificationId);
            Assert.Equal(sesOpen.MessageId, open.Mail.MessageId);
        }
    }
}