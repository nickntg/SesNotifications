using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesSendEventsRepository
    {
        void Save(SesSendEvent sesSendEvent);
        SesSendEvent FindById(long id);
        IList<SesSendEvent> FindByMessageId(string messageId);
        IList<SesSendEvent> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesSendEvent> FindByRecipient(string email);
        IList<SesSendEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
    }
}
