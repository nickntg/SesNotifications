using System;
using System.Linq;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesBounceEventFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var bounce = TestHelpers.GetSesBounceEventModel(dt);

            var sesBounce = bounce.Create(1);

            Assert.Equal(sesBounce.BouncedRecipients, string.Join(',', bounce.Bounce.BouncedRecipients.Select(x => x.EmailAddress).ToArray()));
            Assert.Equal(sesBounce.BounceSubType, bounce.Bounce.BounceSubType);
            Assert.Equal(sesBounce.BounceType, bounce.Bounce.BounceType);
            Assert.Equal(sesBounce.CreatedAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesBounce.FeedbackId, bounce.Bounce.FeedbackId);
            Assert.Equal(sesBounce.ReportingMta, bounce.Bounce.ReportingMta);
            Assert.Equal(sesBounce.SourceArn, bounce.Mail.SourceArn);
            Assert.Equal(sesBounce.NotificationType, bounce.NotificationType);
            Assert.Equal(1, sesBounce.NotificationId);
            Assert.Equal(sesBounce.MessageId, bounce.Mail.MessageId);
        }
    }
}
