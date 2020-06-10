using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesOperationalRepository : Repository, ISesOperationalRepository
    {
        public SesOperationalRepository()
        {
        }

        public SesOperationalRepository(ISession session) : base(session)
        {
        }

        public IList<SesOperational> FindById(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            if (!firstId.HasValue)
            {
                return Session.CreateCriteria<SesOperational>()
                    .AddIfNotEmpty(email, nameof(SesOperational.Recipients))
                    .Add(Restrictions.Ge(nameof(SesOperational.SentAt), start))
                    .Add(Restrictions.Le(nameof(SesOperational.SentAt), end))
                    .AddOrder(Order.Desc(nameof(SesOperational.NotificationId)))
                    .SetMaxResults(pageSize)
                    .List<SesOperational>();
            }

            return Session.CreateCriteria<SesOperational>()
                .AddIfNotEmpty(email, nameof(SesOperational.Recipients))
                .Add(Restrictions.Ge(nameof(SesOperational.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOperational.SentAt), end))
                .Add(Restrictions.Le(nameof(SesOperational.NotificationId), firstId.Value - page * pageSize))
                .AddOrder(Order.Desc(nameof(SesOperational.NotificationId)))
                .SetMaxResults(pageSize)
                .List<SesOperational>();
        }

        public int Count(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesOperational>()
                .AddIfNotEmpty(email, nameof(SesOperational.Recipients))
                .Add(Restrictions.Ge(nameof(SesOperational.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOperational.SentAt), end))
                .List<SesOperational>()
                .Count;
        }
    }
}