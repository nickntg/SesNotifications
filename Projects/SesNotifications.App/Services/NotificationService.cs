using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private const string Open = "open";
        private const string Send = "send";

        private readonly INotificationsRepository _notificationsRepository;
        private readonly ISesBouncesRepository _sesBouncesRepository;
        private readonly ISesComplaintsRepository _sesComplaintsRepository;
        private readonly ISesDeliveriesRepository _sesDeliveriesRepository;
        private readonly ISesOpensEventsRepository _sesOpensEventsRepository;
        private readonly ISesSendEventsRepository _sesSendEventsRepository;
        private readonly ISesDeliveryEventsRepository _sesDeliveryEventsRepository;
        private readonly ISesBounceEventsRepository _sesBounceEventsRepository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationsRepository notificationsRepository,
            ISesBouncesRepository sesBouncesRepository,
            ISesComplaintsRepository sesComplaintsRepository,
            ISesDeliveriesRepository sesDeliveriesRepository,
            ISesOpensEventsRepository sesOpensEventsRepository,
            ISesSendEventsRepository sesSendEventsRepository,
            ISesDeliveryEventsRepository sesDeliveryEventsRepository,
            ISesBounceEventsRepository sesBounceEventsRepository,
            ILogger<NotificationService> logger)
        {
            _notificationsRepository = notificationsRepository;
            _sesBouncesRepository = sesBouncesRepository;
            _sesComplaintsRepository = sesComplaintsRepository;
            _sesDeliveriesRepository = sesDeliveriesRepository;
            _sesOpensEventsRepository = sesOpensEventsRepository;
            _sesSendEventsRepository = sesSendEventsRepository;
            _sesDeliveriesRepository = sesDeliveriesRepository;
            _sesDeliveryEventsRepository = sesDeliveryEventsRepository;
            _sesBounceEventsRepository = sesBounceEventsRepository;
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

            if (ses == null || (string.IsNullOrEmpty(ses.NotificationType) && string.IsNullOrEmpty(ses.EventType)))
            {
                throw new NotSupportedException($"Unsupported message {content.Substring(0, 50)}...");
            }

            if (!string.IsNullOrEmpty(ses.NotificationType))
            {
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
                        throw new NotSupportedException($"Unsupported message {content.Substring(0, 50)}...");
                }
            }

            if (!string.IsNullOrEmpty(ses.EventType))
            {
                switch (ses.EventType.ToLower())
                {
                    case Open:
                        HandleOpenEvent(content);
                        break;
                    case Send:
                        HandleSendEvent(content);
                        break;
                    case Delivery:
                        HandleDeliveryEvent(content);
                        break;
                    case Bounce:
                        HandleBounceEvent(content);
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported message {content.Substring(0, 50)}...");
                }
            }
        }

        private void HandleBounceEvent(string content)
        {
            var bounceEvent = JsonConvert.DeserializeObject<SesBounceEventModel>(content);

            var notification = SaveNotification(bounceEvent.Mail, content);

            _sesBounceEventsRepository.Save(bounceEvent.Create(notification.Id));
        }

        private void HandleDeliveryEvent(string content)
        {
            var delivery = JsonConvert.DeserializeObject<SesDeliveryEventModel>(content);

            var notification = SaveNotification(delivery.Mail, content);

            _sesDeliveryEventsRepository.Save(delivery.Create(notification.Id));
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

        private void HandleOpenEvent(string content)
        {
            var open = JsonConvert.DeserializeObject<SesOpenEventModel>(content);

            var notification = SaveNotification(open.Mail, content);

            _sesOpensEventsRepository.Save(open.Create(notification.Id));
        }

        private void HandleSendEvent(string content)
        {
            var send = JsonConvert.DeserializeObject<SesSendEventModel>(content);

            var notification = SaveNotification(send.Mail, content);

            _sesSendEventsRepository.Save(send.Create(notification.Id));
        }

        private SesNotification SaveNotification(SesMail mail, string content)
        {
            var notification = mail.Create(content);
            _notificationsRepository.Save(notification);
            return notification;
        }
    }
}