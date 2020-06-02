using SesNotifications.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesBounceEventsRepository
    {
        void Save(SesBounceEvent sesBounceEvent);
        SesBounceEvent FindById(long id);
        IList<SesBounceEvent> FindByMessageId(string messageId);
        IList<SesBounceEvent> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesBounceEvent> FindByRecipient(string email);
        IList<SesBounceEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
    }
}
