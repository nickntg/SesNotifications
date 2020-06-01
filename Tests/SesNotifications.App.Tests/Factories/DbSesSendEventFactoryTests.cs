using System;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesSendEventFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var open = TestHelpers.GetSesSendModel(dt);

            var sesOpen = open.Create(1);

            Assert.Equal(sesOpen.Recipients, string.Join(',', open.Mail.Destination));
            Assert.Equal(sesOpen.SentAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesOpen.SendingAccountId, open.Mail.SendingAccountId);
            Assert.Equal(sesOpen.SourceArn, open.Mail.SourceArn);
            Assert.Equal(sesOpen.Source, open.Mail.Source);
            Assert.Equal(sesOpen.SourceIp, open.Mail.SourceIp);
            Assert.Equal(sesOpen.NotificationType, open.EventType);
            Assert.Equal(1, sesOpen.NotificationId);
            Assert.Equal(sesOpen.MessageId, open.Mail.MessageId);
        }
    }
}
