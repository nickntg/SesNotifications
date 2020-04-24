using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface INotificationsRepository
    {
        void Save(SesNotification sesNotification);
        SesNotification FindById(long id);
        IList<SesNotification> FindByMessageId(string messageId);
        IList<SesNotification> FindByReceptionDateRange(DateTime start, DateTime end);
        IList<SesNotification> FindBySentDateRange(DateTime start, DateTime end);
    }
}