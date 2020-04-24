using System;
using System.Linq;
using SesNotifications.App.Factories;
using SesNotifications.App.Tests.Helpers;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbSesComplaintFactoryTests
    {
        [Fact]
        public void Verify()
        {
            var dt = DateTime.UtcNow;
            var complaint = TestHelpers.GetSesComplaintModel(dt);

            var sesComplaint = complaint.Create(1);

            Assert.Null(sesComplaint.ArrivalDate);
            Assert.Equal(1, sesComplaint.NotificationId);
            Assert.Equal(sesComplaint.CreatedAt.Iso8601(), dt.Iso8601());
            Assert.Equal(sesComplaint.FeedbackId, complaint.Complaint.FeedbackId);
            Assert.Equal(sesComplaint.ComplainedRecipients, string.Join(',', complaint.Complaint.ComplainedRecipients.Select(x => x.EmailAddress).ToArray()));
            Assert.Equal(sesComplaint.ComplaintFeedbackType, complaint.Complaint.ComplaintFeedbackType);
            Assert.Equal(sesComplaint.ComplaintSubType, complaint.Complaint.ComplaintSubType);
            Assert.Equal(sesComplaint.UserAgent, complaint.Complaint.UserAgent);
            Assert.Equal(sesComplaint.MessageId, complaint.Mail.MessageId);
            Assert.Equal(sesComplaint.NotificationType, complaint.NotificationType);
        }
    }
}
