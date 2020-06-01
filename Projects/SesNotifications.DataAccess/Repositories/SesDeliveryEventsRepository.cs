using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesDeliveryEventsRepository : Repository, ISesDeliveryEventsRepository
    {
        public SesDeliveryEventsRepository()
        {
        }

        public SesDeliveryEventsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesDeliveryEvent sesDeliveryEvent)
        {
            Session.Save(sesDeliveryEvent);
        }

        public SesDeliveryEvent FindById(long id)
        {
            return Session.Get<SesDeliveryEvent>(id);
        }

        public IList<SesDeliveryEvent> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesDeliveryEvent>()
                .Add(Restrictions.Eq(nameof(SesDeliveryEvent.MessageId), messageId))
                .List<SesDeliveryEvent>();
        }

        public IList<SesDeliveryEvent> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesDeliveryEvent>()
                .Add(Restrictions.Ge(nameof(SesDeliveryEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesDeliveryEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesDeliveryEvent.SentAt)))
                .List<SesDeliveryEvent>();
        }

        public IList<SesDeliveryEvent> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesDeliveryEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesDeliveryEvent.Recipients), email))
                .AddOrder(Order.Desc(nameof(SesDeliveryEvent.SentAt)))
                .List<SesDeliveryEvent>();
        }

        public IList<SesDeliveryEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesDeliveryEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesDeliveryEvent.Recipients), email))
                .Add(Restrictions.Ge(nameof(SesDeliveryEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesDeliveryEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesDeliveryEvent.SentAt)))
                .List<SesDeliveryEvent>();
        }
    }
}
