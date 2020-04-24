using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesBouncesRepository : Repository, ISesBouncesRepository
    {
        public SesBouncesRepository()
        {
        }

        public SesBouncesRepository(ISession session) : base(session)
        {
        }

        public void Save(SesBounce sesDelivery)
        {
            Session.Save(sesDelivery);
        }

        public SesBounce FindById(long id)
        {
            return Session.Get<SesBounce>(id);
        }

        public IList<SesBounce> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesBounce>()
                .Add(Restrictions.Eq(nameof(SesBounce.MessageId), messageId))
                .List<SesBounce>();
        }

        public IList<SesBounce> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesBounce>()
                .Add(Restrictions.Ge(nameof(SesBounce.SentAt), start))
                .Add(Restrictions.Le(nameof(SesBounce.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesBounce.SentAt)))
                .List<SesBounce>();
        }

        public IList<SesBounce> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesBounce>()
                .Add(Restrictions.InsensitiveLike(nameof(SesBounce.BouncedRecipients), email))
                .AddOrder(Order.Desc(nameof(SesBounce.SentAt)))
                .List<SesBounce>();
        }

        public IList<SesBounce> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesBounce>()
                .Add(Restrictions.InsensitiveLike(nameof(SesBounce.BouncedRecipients), email))
                .Add(Restrictions.Ge(nameof(SesBounce.SentAt), start))
                .Add(Restrictions.Le(nameof(SesBounce.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesBounce.SentAt)))
                .List<SesBounce>();
        }
    }
}
