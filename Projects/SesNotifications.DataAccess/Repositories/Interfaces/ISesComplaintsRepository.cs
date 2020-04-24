using System;
using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface ISesComplaintsRepository
    {
        void Save(SesComplaint sesDelivery);
        SesComplaint FindById(long id);
        IList<SesComplaint> FindByMessageId(string messageId);
        IList<SesComplaint> FindBySentDateRange(DateTime start, DateTime end);
        IList<SesComplaint> FindByRecipient(string email);
        IList<SesComplaint> FindByRecipientAndSentDateRange(string email, DateTime start, DateTime end);
        IList<SesComplaint> FindByComplaintSubTypeAndSentDateRange(string complaintSubType, DateTime start, DateTime end);
    }
}