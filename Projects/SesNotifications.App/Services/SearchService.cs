using System;
using System.Collections.Generic;
using NLog;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;
using SesBounce = SesNotifications.DataAccess.Entities.SesBounce;
using SesComplaint = SesNotifications.DataAccess.Entities.SesComplaint;

namespace SesNotifications.App.Services
{
    public class SearchService : ISearchService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly INotificationsRepository _notificationsRepository;
        private readonly ISesBouncesRepository _sesBouncesRepository;
        private readonly ISesComplaintsRepository _sesComplaintsRepository;
        private readonly ISesDeliveriesRepository _sesDeliveriesRepository;
        private readonly ISesOpensEventsRepository _sesOpensEventsRepository;
        private readonly ISesSendEventsRepository _sesSendEventsRepository;
        private readonly ISesDeliveryEventsRepository _sesDeliveryEventsRepository;
        private readonly ISesBounceEventsRepository _sesBounceEventsRepository;
        private readonly ISesComplaintEventsRepository _sesComplaintEventsRepository;
        private readonly ISesOperationalRepository _sesOperationalRepository;

        public SearchService(INotificationsRepository notificationsRepository,
            ISesBouncesRepository sesBouncesRepository,
            ISesComplaintsRepository sesComplaintsRepository,
            ISesDeliveriesRepository sesDeliveriesRepository,
            ISesOpensEventsRepository sesOpensEventsRepository,
            ISesSendEventsRepository sesSendEventsRepository,
            ISesDeliveryEventsRepository sesDeliveryEventsRepository,
            ISesBounceEventsRepository sesBounceEventsRepository,
            ISesComplaintEventsRepository sesComplaintEventsRepository,
            ISesOperationalRepository sesOperationalRepository)
        {
            _notificationsRepository = notificationsRepository;
            _sesBouncesRepository = sesBouncesRepository;
            _sesComplaintsRepository = sesComplaintsRepository;
            _sesDeliveriesRepository = sesDeliveriesRepository;
            _sesOpensEventsRepository = sesOpensEventsRepository;
            _sesSendEventsRepository = sesSendEventsRepository;
            _sesDeliveryEventsRepository = sesDeliveryEventsRepository;
            _sesBounceEventsRepository = sesBounceEventsRepository;
            _sesComplaintEventsRepository = sesComplaintEventsRepository;
            _sesOperationalRepository = sesOperationalRepository;
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

        public IList<SesComplaintEvent> FindComplaintEvents(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesComplaintEventsRepository.FindBySentDateRange(start, end)
                : _sesComplaintEventsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesNotification> FindRaw(DateTime start, DateTime end)
        {
            return _notificationsRepository.FindBySentDateRange(start, end);
        }

        public IList<SesNotification> FindRaw(DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            return _notificationsRepository.FindById(start, end, firstId, page, pageSize);
        }

        public int FindRawCount(DateTime start, DateTime end)
        {
            return _notificationsRepository.Count(start, end);
        }

        public int FindDeliveriesCount(string email, DateTime start, DateTime end)
        {
            return _sesDeliveriesRepository.Count(email, start, end);
        }

        public int FindComplaintsCount(string email, DateTime start, DateTime end)
        {
            return _sesComplaintsRepository.Count(email, start, end);
        }

        public int FindBouncesCount(string email, DateTime start, DateTime end)
        {
            return _sesBouncesRepository.Count(email, start, end);
        }

        public int FindOpenEventsCount(string email, DateTime start, DateTime end)
        {
            return _sesOpensEventsRepository.Count(email, start, end);
        }

        public int FindSendEventsCount(string email, DateTime start, DateTime end)
        {
            return _sesSendEventsRepository.Count(email, start, end);
        }

        public int FindDeliveryEventsCount(string email, DateTime start, DateTime end)
        {
            return _sesDeliveryEventsRepository.Count(email, start, end);
        }

        public int FindBounceEventsCount(string email, DateTime start, DateTime end)
        {
            return _sesBounceEventsRepository.Count(email, start, end);
        }

        public int FindComplaintEventCount(string email, DateTime start, DateTime end)
        {
            return _sesComplaintEventsRepository.Count(email, start, end);
        }

        public int FindOperationalCount(string email, DateTime start, DateTime end)
        {
            return _sesOperationalRepository.Count(email, start, end);
        }

        public IList<SesDelivery> FindDeliveries(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize)
        {
            return _sesDeliveriesRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesComplaint> FindComplaints(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            return _sesComplaintsRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesBounce> FindBounces(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize)
        {
            return _sesBouncesRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesOpenEvent> FindOpenEvents(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize)
        {
            return _sesOpensEventsRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesSendEvent> FindSendEvents(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize)
        {
            return _sesSendEventsRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesDeliveryEvent> FindDeliveryEvents(string email, DateTime start, DateTime end, long? firstId,
            int page, int pageSize)
        {
            return _sesDeliveryEventsRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesBounceEvent> FindBounceEvents(string email, DateTime start, DateTime end, long? firstId,
            int page, int pageSize)
        {
            return _sesBounceEventsRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesComplaintEvent> FindComplaintEvents(string email, DateTime start, DateTime end, long? firstId,
            int page, int pageSize)
        {
            return _sesComplaintEventsRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public IList<SesOperational> FindOperational(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            return _sesOperationalRepository.FindById(email, start, end, firstId, page, pageSize);
        }

        public SesNotification FindRaw(long id)
        {
            return _notificationsRepository.FindById(id);
        }
    }
}
