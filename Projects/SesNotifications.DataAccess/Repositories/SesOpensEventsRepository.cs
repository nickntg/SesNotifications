using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesOpensEventsRepository : Repository, ISesOpensEventsRepository
    {

        public SesOpensEventsRepository()
        {
        }

        public SesOpensEventsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesOpenEvent sesOpenEvent)
        {
            Session.Save(sesOpenEvent);
        }

        public SesOpenEvent FindById(long id)
        {
            return Session.Get<SesOpenEvent>(id);
        }

        public IList<SesOpenEvent> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesOpenEvent>()
                .Add(Restrictions.Eq(nameof(SesOpenEvent.MessageId), messageId))
                .List<SesOpenEvent>();
        }

        public IList<SesOpenEvent> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesOpenEvent>()
                .Add(Restrictions.Ge(nameof(SesOpenEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOpenEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesOpenEvent.SentAt)))
                .List<SesOpenEvent>();
        }

        public IList<SesOpenEvent> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesOpenEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesOpenEvent.Recipients), email))
                .AddOrder(Order.Desc(nameof(SesOpenEvent.SentAt)))
                .List<SesOpenEvent>();
        }

        public IList<SesOpenEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesOpenEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesOpenEvent.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesOpenEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOpenEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesOpenEvent.SentAt)))
                .List<SesOpenEvent>();
        }

        public int Count(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesOpenEvent>()
                .AddIfNotEmpty(email, nameof(SesOpenEvent.Recipients))
                .Add(Restrictions.Ge(nameof(SesOpenEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOpenEvent.SentAt), end))
                .List<SesOpenEvent>()
                .Count;
        }

        public IList<SesOpenEvent> FindById(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            if (!firstId.HasValue)
            {
                return Session.CreateCriteria<SesOpenEvent>()
                    .AddIfNotEmpty(email, nameof(SesOpenEvent.Recipients))
                    .Add(Restrictions.Ge(nameof(SesOpenEvent.SentAt), start))
                    .Add(Restrictions.Le(nameof(SesOpenEvent.SentAt), end))
                    .AddOrder(Order.Desc(nameof(SesOpenEvent.Id)))
                    .SetMaxResults(pageSize)
                    .List<SesOpenEvent>();
            }

            return Session.CreateCriteria<SesOpenEvent>()
                .AddIfNotEmpty(email, nameof(SesOpenEvent.Recipients))
                .Add(Restrictions.Ge(nameof(SesOpenEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOpenEvent.SentAt), end))
                .Add(Restrictions.Le(nameof(SesOpenEvent), firstId.Value - page * pageSize))
                .AddOrder(Order.Desc(nameof(SesDeliveryEvent.Id)))
                .SetMaxResults(pageSize)
                .List<SesOpenEvent>();
        }
    }
}
