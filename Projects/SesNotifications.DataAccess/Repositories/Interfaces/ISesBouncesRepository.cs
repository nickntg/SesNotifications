using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesBouncesRepository
    {
        void Save(SesBounce sesBounce);
        SesBounce FindById(long id);
        IList<SesBounce> FindByMessageId(string messageId);
        IList<SesBounce> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesBounce> FindByRecipient(string email);
        IList<SesBounce> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
        IList<SesBounce> FindById(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize);
        int Count(string email, DateTime start, DateTime end);
    }
}