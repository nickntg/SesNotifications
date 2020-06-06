using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesSendEventsRepository : Repository, ISesSendEventsRepository
    {
        public SesSendEventsRepository()
        {
        }

        public SesSendEventsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesSendEvent sesSendEvent)
        {
            Session.Save(sesSendEvent);
        }

        public SesSendEvent FindById(long id)
        {
            return Session.Get<SesSendEvent>(id);
        }

        public IList<SesSendEvent> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesSendEvent>()
                .Add(Restrictions.Eq(nameof(SesSendEvent.MessageId), messageId))
                .List<SesSendEvent>();
        }

        public IList<SesSendEvent> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesSendEvent>()
                .Add(Restrictions.Ge(nameof(SesSendEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesSendEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesSendEvent.SentAt)))
                .List<SesSendEvent>();
        }

        public IList<SesSendEvent> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesSendEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesSendEvent.Recipients), email))
                .AddOrder(Order.Desc(nameof(SesSendEvent.SentAt)))
                .List<SesSendEvent>();
        }

        public IList<SesSendEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesSendEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesSendEvent.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesSendEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesSendEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesSendEvent.SentAt)))
                .List<SesSendEvent>();
        }

        public int Count(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesSendEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesSendEvent.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesSendEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesSendEvent.SentAt), end))
                .List<SesSendEvent>()
                .Count;
        }

        public IList<SesSendEvent> FindById(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            if (!firstId.HasValue)
            {
                return Session.CreateCriteria<SesSendEvent>()
                    .Add(Restrictions.InsensitiveLike(nameof(SesSendEvent.Recipients), email))
                    .Add(Restrictions.Ge(nameof(SesSendEvent.SentAt), start))
                    .Add(Restrictions.Le(nameof(SesSendEvent.SentAt), end))
                    .AddOrder(Order.Desc(nameof(SesSendEvent.Id)))
                    .SetMaxResults(pageSize)
                    .List<SesSendEvent>();
            }

            return Session.CreateCriteria<SesSendEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesSendEvent.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesSendEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesSendEvent.SentAt), end))
                .Add(Restrictions.Le(nameof(SesSendEvent), firstId.Value - page * pageSize))
                .AddOrder(Order.Desc(nameof(SesSendEvent.Id)))
                .SetMaxResults(pageSize)
                .List<SesSendEvent>();
        }
    }
}