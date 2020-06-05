using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class SesComplaintEventsRepository : Repository, ISesComplaintEventsRepository
    {
        public SesComplaintEventsRepository()
        {
        }

        public SesComplaintEventsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesComplaintEvent sesComplaintEvent)
        {
            Session.Save(sesComplaintEvent);
        }

        public SesComplaintEvent FindById(long id)
        {
            return Session.Get<SesComplaintEvent>(id);
        }

        public IList<SesComplaintEvent> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesComplaintEvent>()
                .Add(Restrictions.Eq(nameof(SesComplaintEvent.MessageId), messageId))
                .List<SesComplaintEvent>();
        }

        public IList<SesComplaintEvent> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesComplaintEvent>()
                .Add(Restrictions.Ge(nameof(SesComplaintEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesComplaintEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesComplaintEvent.SentAt)))
                .List<SesComplaintEvent>();
        }

        public IList<SesComplaintEvent> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesComplaintEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesComplaintEvent.ComplainedRecipients), email))
                .AddOrder(Order.Desc(nameof(SesComplaintEvent.SentAt)))
                .List<SesComplaintEvent>();
        }

        public IList<SesComplaintEvent> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesComplaintEvent>()
                .Add(Restrictions.InsensitiveLike(nameof(SesComplaintEvent.ComplainedRecipients), email))
                .Add(Restrictions.Ge(nameof(SesComplaintEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesComplaintEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesComplaintEvent.SentAt)))
                .List<SesComplaintEvent>();
        }

        public IList<SesComplaintEvent> FindByComplaintSubTypeAndSentDateRange(string complaintSubType, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesComplaintEvent>()
                .Add(Restrictions.Eq(nameof(SesComplaintEvent.ComplaintSubType), complaintSubType))
                .Add(Restrictions.Ge(nameof(SesComplaintEvent.SentAt), start))
                .Add(Restrictions.Le(nameof(SesComplaintEvent.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesComplaintEvent.SentAt)))
                .List<SesComplaintEvent>();
        }
    }
}