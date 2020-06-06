using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesDeliveriesRepository : Repository, ISesDeliveriesRepository
    {
        public SesDeliveriesRepository()
        {
        }

        public SesDeliveriesRepository(ISession session) : base(session)
        {
        }

        public void Save(SesDelivery sesDelivery)
        {
            Session.Save(sesDelivery);
        }

        public SesDelivery FindById(long id)
        {
            return Session.Get<SesDelivery>(id);
        }

        public IList<SesDelivery> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesDelivery>()
                .Add(Restrictions.Eq(nameof(SesDelivery.MessageId), messageId))
                .List<SesDelivery>();
        }

        public IList<SesDelivery> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesDelivery>()
                .Add(Restrictions.Ge(nameof(SesDelivery.SentAt), start))
                .Add(Restrictions.Le(nameof(SesDelivery.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesDelivery.SentAt)))
                .List<SesDelivery>();
        }

        public IList<SesDelivery> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesDelivery>()
                .Add(Restrictions.InsensitiveLike(nameof(SesDelivery.Recipients), email))
                .AddOrder(Order.Desc(nameof(SesDelivery.SentAt)))
                .List<SesDelivery>();
        }

        public IList<SesDelivery> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesDelivery>()
                .Add(Restrictions.InsensitiveLike(nameof(SesDelivery.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesDelivery.SentAt), start))
                .Add(Restrictions.Le(nameof(SesDelivery.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesDelivery.SentAt)))
                .List<SesDelivery>();
        }

        public int Count(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesDelivery>()
                .Add(Restrictions.InsensitiveLike(nameof(SesDelivery.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesDelivery.SentAt), start))
                .Add(Restrictions.Le(nameof(SesDelivery.SentAt), end))
                .List<SesDelivery>()
                .Count;
        }

        public IList<SesDelivery> FindById(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            if (!firstId.HasValue)
            {
                return Session.CreateCriteria<SesDelivery>()
                    .Add(Restrictions.InsensitiveLike(nameof(SesDelivery.Recipients), email))
                    .Add(Restrictions.Ge(nameof(SesDelivery.SentAt), start))
                    .Add(Restrictions.Le(nameof(SesDelivery.SentAt), end))
                    .AddOrder(Order.Desc(nameof(SesDelivery.Id)))
                    .SetMaxResults(pageSize)
                    .List<SesDelivery>();
            }

            return Session.CreateCriteria<SesDelivery>()
                .Add(Restrictions.InsensitiveLike(nameof(SesDelivery.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesDelivery.SentAt), start))
                .Add(Restrictions.Le(nameof(SesDelivery.SentAt), end))
                .Add(Restrictions.Le(nameof(SesDelivery.Id), firstId.Value - page * pageSize))
                .AddOrder(Order.Desc(nameof(SesDelivery.Id)))
                .SetMaxResults(pageSize)
                .List<SesDelivery>();
        }

    }
}
