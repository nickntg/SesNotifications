﻿using System;
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
        private readonly ISesOpensRepository _sesOpensRepository;
        private readonly ISesSendsRepository _sesSendsRepository;
        private readonly ILogger<SearchService> _logger;

        public SearchService(INotificationsRepository notificationsRepository,
            ISesBouncesRepository sesBouncesRepository,
            ISesComplaintsRepository sesComplaintsRepository,
            ISesDeliveriesRepository sesDeliveriesRepository,
            ISesOpensRepository sesOpensRepository,
            ISesSendsRepository sesSendsRepository,
            ILogger<SearchService> logger)
        {
            _notificationsRepository = notificationsRepository;
            _sesBouncesRepository = sesBouncesRepository;
            _sesComplaintsRepository = sesComplaintsRepository;
            _sesDeliveriesRepository = sesDeliveriesRepository;
            _sesOpensRepository = sesOpensRepository;
            _sesSendsRepository = sesSendsRepository;
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

        public IList<SesOpen> FindOpens(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesOpensRepository.FindBySentDateRange(start, end)
                : _sesOpensRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
        }

        public IList<SesSend> FindSends(string email, DateTime start, DateTime end)
        {
            return string.IsNullOrEmpty(email)
                ? _sesSendsRepository.FindBySentDateRange(start, end)
                : _sesSendsRepository.FindByRecipientAndSentDateRange($"%{email}%", start, end);
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
