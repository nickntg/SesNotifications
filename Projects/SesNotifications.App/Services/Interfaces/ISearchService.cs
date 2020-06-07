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
        int FindRawCount(DateTime start, DateTime end);
        int FindDeliveriesCount(string email, DateTime start, DateTime end);
        int FindComplaintsCount(string email, DateTime start, DateTime end);
        int FindBouncesCount(string email, DateTime start, DateTime end);
        int FindOpenEventsCount(string email, DateTime start, DateTime end);
        int FindSendEventsCount(string email, DateTime start, DateTime end);
        int FindDeliveryEventsCount(string email, DateTime start, DateTime end);
        int FindBounceEventsCount(string email, DateTime start, DateTime end);
        int FindComplaintEventCount(string email, DateTime start, DateTime end);
        IList<SesDelivery> FindDeliveries(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesComplaint> FindComplaints(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesBounce> FindBounces(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesOpenEvent> FindOpenEvents(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesSendEvent> FindSendEvents(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesDeliveryEvent> FindDeliveryEvents(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesBounceEvent> FindBounceEvents(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        IList<SesComplaintEvent> FindComplaintEvents(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize);
        SesNotification FindRaw(long id);
    }
}