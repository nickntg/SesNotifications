using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesSendsRepository
    {
        void Save(SesSend sesSend);
        SesSend FindById(long id);
        IList<SesSend> FindByMessageId(string messageId);
        IList<SesSend> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesSend> FindByRecipient(string email);
        IList<SesSend> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
    }
}
