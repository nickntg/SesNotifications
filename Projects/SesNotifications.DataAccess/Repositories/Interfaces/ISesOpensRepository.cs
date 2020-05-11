using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesOpensRepository
    {
        void Save(SesOpen sesOpen);
        SesOpen FindById(long id);
        IList<SesOpen> FindByMessageId(string messageId);
        IList<SesOpen> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesOpen> FindByRecipient(string email);
        IList<SesOpen> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
    }
}
