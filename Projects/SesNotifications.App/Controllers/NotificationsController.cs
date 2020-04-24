using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Services.Interfaces;

namespace SesNotifications.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] object o)
        {
            var content = o.ToString();

            _notificationService.HandleNotification(content);

            return Ok();
        }
    }
}