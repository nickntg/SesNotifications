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

        public IList<SesNotification> FindById(DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            if (!firstId.HasValue)
            {
                return Session.CreateCriteria<SesNotification>()
                    .Add(Restrictions.Ge(nameof(SesNotification.SentAt), start))
                    .Add(Restrictions.Le(nameof(SesNotification.SentAt), end))
                    .AddOrder(Order.Desc(nameof(SesNotification.Id)))
                    .SetMaxResults(pageSize)
                    .List<SesNotification>();
            }

            return Session.CreateCriteria<SesNotification>()
                .Add(Restrictions.Ge(nameof(SesNotification.SentAt), start))
                .Add(Restrictions.Le(nameof(SesNotification.SentAt), end))
                .Add(Restrictions.Le(nameof(SesNotification.Id), firstId.Value - page * pageSize))
                .AddOrder(Order.Desc(nameof(SesNotification.Id)))
                .SetMaxResults(pageSize)
                .List<SesNotification>();
        }

        public int Count(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesNotification>()
                .Add(Restrictions.Ge(nameof(SesNotification.SentAt), start))
                .Add(Restrictions.Le(nameof(SesNotification.SentAt), end))
                .List<SesNotification>()
                .Count;
        }

        public IList<SesNotification> FindByIdRange(long firstId, int count)
        {
            return Session.CreateCriteria<SesNotification>()
                .Add(Restrictions.Ge(nameof(SesNotification.Id), firstId))
                .AddOrder(Order.Asc(nameof(SesNotification.Id)))
                .SetMaxResults(count)
                .List<SesNotification>();
        }
    }
}