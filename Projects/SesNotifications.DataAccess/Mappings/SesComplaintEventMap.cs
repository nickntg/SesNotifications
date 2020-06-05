using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesComplaintEventMap : SesCommonMap<SesComplaintEvent>
    {
        public SesComplaintEventMap()
        {
            Table("ses_notifications.complaintevents");
            MapCommon();
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.ComplaintSubType).Column("complaint_sub_type");
            Map(x => x.ComplaintFeedbackType).Column("complaint_feedback_type");
            Map(x => x.FeedbackId).Column("feedback_id");
            Map(x => x.ComplainedRecipients).Column("complained_recipients");
            Map(x => x.ArrivalDate).Column("arrival_date");
        }
    }
}