using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesOpensEventsRepository
    {
        void Save(SesOpenEvent sesOpenEvent);
        SesOpenEvent FindById(long id);
        IList<SesOpenEvent> FindByMessageId(string messageId);
        IList<SesOpenEvent> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesOpenEvent> FindByRecipient(string email);
        IList<SesOpenEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
        IList<SesOpenEvent> FindById(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize);
        int Count(string email, DateTime start, DateTime end);
    }
}
