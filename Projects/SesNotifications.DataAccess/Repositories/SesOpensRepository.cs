using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesOpensRepository : Repository, ISesOpensRepository
    {

        public SesOpensRepository()
        {
        }

        public SesOpensRepository(ISession session) : base(session)
        {
        }

        public void Save(SesOpen sesOpen)
        {
            Session.Save(sesOpen);
        }

        public SesOpen FindById(long id)
        {
            return Session.Get<SesOpen>(id);
        }

        public IList<SesOpen> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesOpen>()
                .Add(Restrictions.Eq(nameof(SesOpen.MessageId), messageId))
                .List<SesOpen>();
        }

        public IList<SesOpen> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesOpen>()
                .Add(Restrictions.Ge(nameof(SesOpen.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOpen.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesOpen.SentAt)))
                .List<SesOpen>();
        }

        public IList<SesOpen> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesOpen>()
                .Add(Restrictions.InsensitiveLike(nameof(SesOpen.Recipients), email))
                .AddOrder(Order.Desc(nameof(SesOpen.SentAt)))
                .List<SesOpen>();
        }

        public IList<SesOpen> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesOpen>()
                .Add(Restrictions.InsensitiveLike(nameof(SesOpen.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesOpen.SentAt), start))
                .Add(Restrictions.Le(nameof(SesOpen.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesOpen.SentAt)))
                .List<SesOpen>();
        }

    }
}
