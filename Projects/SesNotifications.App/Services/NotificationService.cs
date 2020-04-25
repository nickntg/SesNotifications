using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SesNotifications.App.Controllers;
using SesNotifications.App.Factories;
using SesNotifications.App.Models;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.App.Services
{
    public class NotificationService : INotificationService
    {
        private const string Bounce = "bounce";
        private const string Delivery = "delivery";
        private const string Complaint = "complaint";

        private readonly INotificationsRepository _notificationsRepository;
        private readonly ISesBouncesRepository _sesBouncesRepository;
        private readonly ISesComplaintsRepository _sesComplaintsRepository;
        private readonly ISesDeliveriesRepository _sesDeliveriesRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationsRepository notificationsRepository,
            ISesBouncesRepository sesBouncesRepository,
            ISesComplaintsRepository sesComplaintsRepository,
            ISesDeliveriesRepository sesDeliveriesRepository,
            ILogger<NotificationService> logger)
        {
            _notificationsRepository = notificationsRepository;
            _sesBouncesRepository = sesBouncesRepository;
            _sesComplaintsRepository = sesComplaintsRepository;
            _sesDeliveriesRepository = sesDeliveriesRepository;
            _logger = logger;
        }

        public void HandleNotification(string content)
        {
            try
            {
                _logger.LogDebug($"Handling {content}");
                HandleNotificationInternal(content);
                _logger.LogDebug("Handled");
            }
            catch (Exception e)
            {
                _logger.LogError(e,$"Error occurred while handling content \r\n{content}");
                throw;
            }
        }

        private void HandleNotificationInternal(string content)
        {
            var ses = JsonConvert.DeserializeObject<Ses>(content);
            switch (ses.NotificationType.ToLower())
            {
                case Delivery:
                    HandleDelivery(content);
                    break;
                case Complaint:
                    HandleComplaint(content);
                    break;
                case Bounce:
                    HandleBounce(content);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported notification type {ses.NotificationType}");
            }
        }

        private void HandleDelivery(string content)
        {
            var delivery = JsonConvert.DeserializeObject<SesDeliveryModel>(content);

            var notification = SaveNotification(delivery.Mail, content);

            _sesDeliveriesRepository.Save(delivery.Create(notification.Id));
        }

        private void HandleComplaint(string content)
        {
            var complaint = JsonConvert.DeserializeObject<SesComplaintModel>(content);

            var notification = SaveNotification(complaint.Mail, content);

            _sesComplaintsRepository.Save(complaint.Create(notification.Id));
        }

        private void HandleBounce(string content)
        {
            var bounce = JsonConvert.DeserializeObject<SesBounceModel>(content);

            var notification = SaveNotification(bounce.Mail, content);

            _sesBouncesRepository.Save(bounce.Create(notification.Id));
        }

        private SesNotification SaveNotification(SesMail mail, string content)
        {
            var notification = mail.Create(content);
            _notificationsRepository.Save(notification);
            return notification;
        }
    }
}