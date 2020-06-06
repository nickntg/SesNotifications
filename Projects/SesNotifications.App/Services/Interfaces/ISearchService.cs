using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;
using SesBounce = SesNotifications.DataAccess.Entities.SesBounce;
using SesComplaint = SesNotifications.DataAccess.Entities.SesComplaint;

namespace SesNotifications.App.Services.Interfaces
{
    public interface ISearchService
    {
        IList<SesDelivery> FindDeliveries(string email, DateTime start, DateTime end);
        IList<SesComplaint> FindComplaints(string email, DateTime start, DateTime end);
        IList<SesBounce> FindBounces(string email, DateTime start, DateTime end);
        IList<SesOpenEvent> FindOpenEvents(string email, DateTime start, DateTime end);
        IList<SesSendEvent> FindSendEvents(string email, DateTime start, DateTime end);
        IList<SesDeliveryEvent> FindDeliveryEvents(string email, DateTime start, DateTime end);
        IList<SesBounceEvent> FindBounceEvents(string email, DateTime start, DateTime end);
        IList<SesComplaintEvent> FindComplaintEvents(string email, DateTime start, DateTime end);
        IList<SesNotification> FindRaw(DateTime start, DateTime end);
        IList<SesNotification> FindRaw(DateTime start, DateTime end, long? firstId, int page, int pageSize);
        int Count(DateTime start, DateTime end);
        SesNotification FindRaw(long id);
    }
}