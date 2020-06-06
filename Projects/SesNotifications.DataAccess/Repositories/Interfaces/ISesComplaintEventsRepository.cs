using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesComplaintEventsRepository
    {
        void Save(SesComplaintEvent sesComplaintEvent);
        SesComplaintEvent FindById(long id);
        IList<SesComplaintEvent> FindByMessageId(string messageId);
        IList<SesComplaintEvent> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesComplaintEvent> FindByRecipient(string email);
        IList<SesComplaintEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
        IList<SesComplaintEvent> FindByComplaintSubTypeAndSentDateRange(string complaintSubType, DateTime start, DateTime end);
        IList<SesComplaintEvent> FindById(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize);
        int Count(string email, DateTime start, DateTime end);
    }
}