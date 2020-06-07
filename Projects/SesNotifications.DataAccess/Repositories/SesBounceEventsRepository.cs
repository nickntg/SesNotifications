using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesBounceEventsRepository : Repository, ISesBounceEventsRepository
    {
        public SesBounceEventsRepository()
        {
        }

        public SesBounceEventsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesBounceEvent sesBounceEvent)
        {
            Session.Save(sesBounceEvent);
        }

        public SesBounceEvent FindById(long id)
        {
            return Session.Get<SesBounceEvent>(id);
        }

        public IList<SesBounceEvent> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesBounceEvent>()
                .Add(Restrictions.Eq(nameof(SesBounceEvent.MessageId), messageId))
                .List<SesBounceEvent>();
        }

        public IList<SesBounceEvent> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesBounceEvent>()
                .Add(Restrictions.Ge(nameof(SesBounceEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesBounceEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesBounceEvent.SentAt)))
                .List<SesBounceEvent>();
        }

        public IList<SesBounceEvent> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesBounceEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesBounceEvent.BouncedRecipients), email))
                .AddOrder(Order.Desc(nameof(SesBounceEvent.SentAt)))
                .List<SesBounceEvent>();
        }

        public IList<SesBounceEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesBounceEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesBounceEvent.BouncedRecipients), email))
                .Add(Restrictions.Ge(nameof(SesBounceEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesBounceEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesBounceEvent.SentAt)))
                .List<SesBounceEvent>();
        }

        public int Count(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesBounceEvent>()
                .AddIfNotEmpty(email, nameof(SesBounceEvent.BouncedRecipients))
                .Add(Restrictions.Ge(nameof(SesBounceEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesBounceEvent.SentAt), end))
                .List<SesBounceEvent>()
                .Count;
        }

        public IList<SesBounceEvent> FindById(string email, DateTime start, DateTime end, long? firstId, int page, int pageSize)
        {
            if (!firstId.HasValue)
            {
                return Session.CreateCriteria<SesBounceEvent>()
                    .AddIfNotEmpty(email, nameof(SesBounceEvent.BouncedRecipients))
                    .Add(Restrictions.Ge(nameof(SesBounceEvent.SentAt), start))
                    .Add(Restrictions.Le(nameof(SesBounceEvent.SentAt), end))
                    .AddOrder(Order.Desc(nameof(SesBounceEvent.Id)))
                    .SetMaxResults(pageSize)
                    .List<SesBounceEvent>();
            }

            return Session.CreateCriteria<SesBounceEvent>()
                .AddIfNotEmpty(email, nameof(SesBounceEvent.BouncedRecipients))
                .Add(Restrictions.Ge(nameof(SesBounceEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesBounceEvent.SentAt), end))
                .Add(Restrictions.Le(nameof(SesBounceEvent.Id), firstId.Value - page * pageSize))
                .AddOrder(Order.Desc(nameof(SesBounceEvent.Id)))
                .SetMaxResults(pageSize)
                .List<SesBounceEvent>();
        }
    }
}
