using System;
using System.Linq;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesComplaintEventFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var complaint = TestHelpers.GetSesComplaintEventModel(dt);

            var sesComplaintEvent = complaint.Create(1);

            Assert.Null(sesComplaintEvent.ArrivalDate);
            Assert.Equal(1, sesComplaintEvent.NotificationId);
            Assert.Equal(sesComplaintEvent.CreatedAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesComplaintEvent.FeedbackId, complaint.Complaint.FeedbackId);
            Assert.Equal(sesComplaintEvent.ComplainedRecipients, string.Join(',', complaint.Complaint.ComplainedRecipients.Select(x => x.EmailAddress).ToArray()));
            Assert.Equal(sesComplaintEvent.ComplaintFeedbackType, complaint.Complaint.ComplaintFeedbackType);
            Assert.Equal(sesComplaintEvent.ComplaintSubType, complaint.Complaint.ComplaintSubType);
            Assert.Equal(sesComplaintEvent.MessageId, complaint.Mail.MessageId);
            Assert.Equal(sesComplaintEvent.NotificationType, complaint.NotificationType);
        }
    }
}
