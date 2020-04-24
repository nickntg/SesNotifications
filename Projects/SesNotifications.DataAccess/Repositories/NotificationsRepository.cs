using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class NotificationsRepository : Repository, INotificationsRepository
    {
        public NotificationsRepository()
        {
        }

        public NotificationsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesNotification sesNotification)
        {
            Session.Save(sesNotification);
        }

        public SesNotification FindById(long id)
        {
            return Session.Get<SesNotification>(id);
        }

        public IList<SesNotification> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesNotification>()
                .Add(Restrictions.Eq(nameof(SesNotification.MessageId), messageId))
                .List<SesNotification>();
        }

        public IList<SesNotification> FindByReceptionDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesNotification>()
                .Add(Restrictions.Ge(nameof(SesNotification.ReceivedAt), start))
                .Add(Restrictions.Le(nameof(SesNotification.ReceivedAt), end))
                .AddOrder(Order.Desc(nameof(SesNotification.ReceivedAt)))
                .List<SesNotification>();
        }

        public IList<SesNotification> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesNotification>()
                .Add(Restrictions.Ge(nameof(SesNotification.SentAt), start))
                .Add(Restrictions.Le(nameof(SesNotification.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesNotification.SentAt)))
                .List<SesNotification>();
        }
    }
}