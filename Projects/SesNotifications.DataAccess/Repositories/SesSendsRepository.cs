using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesSendsRepository : Repository, ISesSendsRepository
    {
        public SesSendsRepository()
        {
        }

        public SesSendsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesSend sesSend)
        {
            Session.Save(sesSend);
        }

        public SesSend FindById(long id)
        {
            return Session.Get<SesSend>(id);
        }

        public IList<SesSend> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesSend>()
                .Add(Restrictions.Eq(nameof(SesSend.MessageId), messageId))
                .List<SesSend>();
        }

        public IList<SesSend> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesSend>()
                .Add(Restrictions.Ge(nameof(SesSend.SentAt), start))
                .Add(Restrictions.Le(nameof(SesSend.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesSend.SentAt)))
                .List<SesSend>();
        }

        public IList<SesSend> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesSend>()
                .Add(Restrictions.InsensitiveLike(nameof(SesSend.Recipients), email))
                .AddOrder(Order.Desc(nameof(SesSend.SentAt)))
                .List<SesSend>();
        }

        public IList<SesSend> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesSend>()
                .Add(Restrictions.InsensitiveLike(nameof(SesSend.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesSend.SentAt), start))
                .Add(Restrictions.Le(nameof(SesSend.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesSend.SentAt)))
                .List<SesSend>();
        }
    }
}