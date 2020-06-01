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
        IList<SesOpenEvent> FindOpens(string email, DateTime start, DateTime end);
        IList<SesSendEvent> FindSends(string email, DateTime start, DateTime end);
        IList<SesNotification> FindRaw(DateTime start, DateTime end);
        SesNotification FindRaw(long id);
    }
}