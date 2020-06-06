using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesDeliveriesRepository
    {
        void Save(SesDelivery sesDelivery);
        SesDelivery FindById(long id);
        IList<SesDelivery> FindByMessageId(string messageId);
        IList<SesDelivery> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesDelivery> FindByRecipient(string email);
        IList<SesDelivery> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
        IList<SesDelivery> FindById(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize);
        int Count(string email, DateTime start, DateTime end);
    }
}