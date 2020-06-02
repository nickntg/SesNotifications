using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;
using SesBounce = SesNotifications.DataAccess.Entities.SesBounce;
using SesComplaint = SesNotifications.DataAccess.Entities.SesComplaint;

namespace SesNotifications.App.Services
{
    public class SearchService : ISearchService
    {
        private readonly INotificationsRepository _notificationsRepository;
        private readonly ISesBouncesRepository _sesBouncesRepository;
        private readonly ISesComplaintsRepository _sesComplaintsRepository;
        private readonly ISesDeliveriesRepository _sesDeliveriesRepository;
        private readonly ISesOpensEventsRepository _sesOpensEventsRepository;
        private readonly ISesSendEventsRepository _sesSendEventsRepository;
        private readonly ISesDeliveryEventsRepository _sesDeliveryEventsRepository;
        private readonly ISesBounceEventsRepository _sesBounceEventsRepository;
        private readonly ILogger<SearchService> _logger;

        public SearchService(INotificationsRepository notificationsRepository,
            ISesBouncesRepository sesBouncesRepository,
            ISesComplaintsRepository sesComplaintsRepository,
            ISesDeliveriesRepository sesDeliveriesRepository,
            ISesOpensEventsRepository sesOpensEventsRepository,
            ISesSendEventsRepository sesSendEventsRepository,
            ISesDeliveryEventsRepository sesDeliveryEventsRepository,
            ISesBounceEventsRepository sesBounceEventsRepository,
            ILogger<SearchService> logger)
        {
            _notificationsRepository = notificationsRepository;
            _sesBouncesRepository = sesBouncesRepository;
            _sesComplaintsRepository = sesComplaintsRepository;
            _sesDeliveriesRepository = sesDeliveriesRepository;
            _sesOpensEventsRepository = sesOpensEventsRepository;
            _sesSendEventsRepository = sesSendEventsRepository;
            _sesDeliveryEventsRepository = sesDeliveryEventsRepository;
            _sesBounceEventsRepository = sesBounceEventsRepository;
            _logger = logger;
        }

        public IList<SesDelivery> FindDeliveries(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email) 
                ? _sesDeliveriesRepository.FindBySentDateRange(start, end) 
                : _sesDeliveriesRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesComplaint> FindComplaints(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesComplaintsRepository.FindBySentDateRange(start, end)
                : _sesComplaintsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesBounce> FindBounces(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesBouncesRepository.FindBySentDateRange(start, end)
                : _sesBouncesRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesOpenEvent> FindOpenEvents(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesOpensEventsRepository.FindBySentDateRange(start, end)
                : _sesOpensEventsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesSendEvent> FindSendEvents(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesSendEventsRepository.FindBySentDateRange(start, end)
                : _sesSendEventsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesDeliveryEvent> FindDeliveryEvents(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesDeliveryEventsRepository.FindBySentDateRange(start, end)
                : _sesDeliveryEventsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesBounceEvent> FindBounceEvents(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesBounceEventsRepository.FindBySentDateRange(start, end)
                : _sesBounceEventsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesNotification> FindRaw(DateTime start, DateTime end)
        {
            return _notificationsRepository.FindBySentDateRange(start, end);
        }

        public SesNotification FindRaw(long id)
        {
            return _notificationsRepository.FindById(id);
        }
    }
}
