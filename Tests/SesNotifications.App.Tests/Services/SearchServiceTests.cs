using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using SesNotifications.App.Services;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;
using Xunit;

namespace SesNotifications.App.Tests.Services
{
    public class SearchServiceTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("mail@here.com", true)]
        public void VerifyFindDeliveries(string email, bool expectedMailQuery)
        {
            var mockSesDeliveries = new Mock<ISesDeliveriesRepository>(MockBehavior.Strict);
            mockSesDeliveries.Setup(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesDelivery>());
            mockSesDeliveries.Setup(x =>
                    x.FindByRecipientAndSentDateRange(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesDelivery>());
            var mockLogger = new Mock<ILogger<SearchService>>(MockBehavior.Loose);

            var service = new SearchService(null, null, null, mockSesDeliveries.Object, null, null, null,
                null, mockLogger.Object);

            service.FindDeliveries(email, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(1));

            mockSesDeliveries.Verify(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                expectedMailQuery ? Times.Exactly(0) : Times.Exactly(1));
            mockSesDeliveries.Verify(x =>
                    x.FindByRecipientAndSentDateRange(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                expectedMailQuery ? Times.Exactly(1) : Times.Exactly(0));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("mail@here.com", true)]
        public void VerifyFindBounces(string email, bool expectedMailQuery)
        {
            var mockSesBounces = new Mock<ISesBouncesRepository>(MockBehavior.Strict);
            mockSesBounces.Setup(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesBounce>());
            mockSesBounces.Setup(x =>
                    x.FindByRecipientAndSentDateRange(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesBounce>());
            var mockLogger = new Mock<ILogger<SearchService>>(MockBehavior.Loose);

            var service = new SearchService(null, mockSesBounces.Object, null, null, null, null, null,
                null, mockLogger.Object);

            service.FindBounces(email, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(1));

            mockSesBounces.Verify(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                expectedMailQuery ? Times.Exactly(0) : Times.Exactly(1));
            mockSesBounces.Verify(x =>
                    x.FindByRecipientAndSentDateRange(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                expectedMailQuery ? Times.Exactly(1) : Times.Exactly(0));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("mail@here.com", true)]
        public void VerifyFindComplaints(string email, bool expectedMailQuery)
        {
            var mockSesComplaints = new Mock<ISesComplaintsRepository>(MockBehavior.Strict);
            mockSesComplaints.Setup(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesComplaint>());
            mockSesComplaints.Setup(x =>
                    x.FindByRecipientAndSentDateRange(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesComplaint>());
            var mockLogger = new Mock<ILogger<SearchService>>(MockBehavior.Loose);

            var service = new SearchService(null, null, mockSesComplaints.Object, null, null, null, null,
                null, mockLogger.Object);

            service.FindComplaints(email, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(1));

            mockSesComplaints.Verify(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                expectedMailQuery ? Times.Exactly(0) : Times.Exactly(1));
            mockSesComplaints.Verify(x =>
                    x.FindByRecipientAndSentDateRange(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                expectedMailQuery ? Times.Exactly(1) : Times.Exactly(0));
        }

        [Fact]
        public void VerifyFindRaw()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            mockNotifications.Setup(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<SesNotification>());
            var mockLogger = new Mock<ILogger<SearchService>>(MockBehavior.Loose);

            var service = new SearchService(mockNotifications.Object, null, null, null, null, null, null,
                null, mockLogger.Object);

            service.FindRaw(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(1));

            mockNotifications.Verify(x => x.FindBySentDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(1));
        }

        [Fact]
        public void VerifyFindOneRaw()
        {
            var mockNotifications = new Mock<INotificationsRepository>(MockBehavior.Strict);
            mockNotifications.Setup(x => x.FindById(1)).Returns(new SesNotification());
            var mockLogger = new Mock<ILogger<SearchService>>(MockBehavior.Loose);

            var service = new SearchService(mockNotifications.Object, null, null, null, null, null, null,
                null, mockLogger.Object);

            service.FindRaw(1);

            mockNotifications.Verify(x => x.FindById(1), Times.Exactly(1));
        }
    }
}