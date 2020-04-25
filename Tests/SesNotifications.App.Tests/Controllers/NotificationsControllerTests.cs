using Microsoft.AspNetCore.Mvc;
using Moq;
using SesNotifications.App.Controllers;
using SesNotifications.App.Services.Interfaces;
using Xunit;

namespace SesNotifications.App.Tests.Controllers
{
    public class NotificationsControllerTests
    {
        [Fact]
        public void Verify()
        {
            var mockService = new Mock<INotificationService>(MockBehavior.Strict);
            mockService.Setup(x => x.HandleNotification(It.IsAny<string>()));

            var controller = new NotificationsController(mockService.Object);

            var result = controller.Post("something");
            Assert.NotNull(result);

            var response = result as OkResult;
            Assert.NotNull(response);

            mockService.Verify(x => x.HandleNotification(It.IsAny<string>()), Times.Exactly(1));
        }
    }
}
