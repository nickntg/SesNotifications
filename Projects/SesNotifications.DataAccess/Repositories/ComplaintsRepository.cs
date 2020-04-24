using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class ComplaintsRepository : Repository, IComplaintsRepository
    {
        public ComplaintsRepository()
        {
        }

        public ComplaintsRepository(ISession session) : base(session)
        {
        }

        public void Save(SesComplaint sesDelivery)
        {
            Session.Save(sesDelivery);
        }

        public SesComplaint FindById(long id)
        {
            return Session.Get<SesComplaint>(id);
        }

        public IList<SesComplaint> FindByMessageId(string messageId)
        {
            return Session.CreateCriteria<SesComplaint>()
                .Add(Restrictions.Eq(nameof(SesComplaint.MessageId), messageId))
                .List<SesComplaint>();
        }

        public IList<SesComplaint> FindBySentDateRange(DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesComplaint>()
                .Add(Restrictions.Ge(nameof(SesComplaint.SentAt), start))
                .Add(Restrictions.Le(nameof(SesComplaint.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesComplaint.SentAt)))
                .List<SesComplaint>();
        }

        public IList<SesComplaint> FindByRecipient(string email)
        {
            return Session.CreateCriteria<SesComplaint>()
                .Add(Restrictions.InsensitiveLike(nameof(SesComplaint.ComplainedRecipients), email))
                .AddOrder(Order.Desc(nameof(SesComplaint.SentAt)))
                .List<SesComplaint>();
        }

        public IList<SesComplaint> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesComplaint>()
                .Add(Restrictions.InsensitiveLike(nameof(SesComplaint.ComplainedRecipients), email))
                .Add(Restrictions.Ge(nameof(SesComplaint.SentAt), start))
                .Add(Restrictions.Le(nameof(SesComplaint.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesComplaint.SentAt)))
                .List<SesComplaint>();
        }

        public IList<SesComplaint> FindByComplaintSubTypeAndSentDateRange(string complaintSubType, DateTime start, DateTime end)
        {
            return Session.CreateCriteria<SesComplaint>()
                .Add(Restrictions.Eq(nameof(SesComplaint.ComplaintSubType), complaintSubType))
                .Add(Restrictions.Ge(nameof(SesComplaint.SentAt), start))
                .Add(Restrictions.Le(nameof(SesComplaint.SentAt), end))
                .AddOrder(Order.Desc(nameof(SesComplaint.SentAt)))
                .List<SesComplaint>();
        }
    }
}
