using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesDeliveryEventsRepository
    {
        void Save(SesDeliveryEvent sesDeliveryEvent);
        SesDeliveryEvent FindById(long id);
        IList<SesDeliveryEvent> FindByMessageId(string messageId);
        IList<SesDeliveryEvent> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesDeliveryEvent> FindByRecipient(string email);
        IList<SesDeliveryEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
        IList<SesDeliveryEvent> FindById(string email, DateTime start, DateTime end, long? firstId, int page,
            int pageSize);
        int Count(string email, DateTime start, DateTime end);
    }
}
