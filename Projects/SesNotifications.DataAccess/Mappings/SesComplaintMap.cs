using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesComplaintMap : SesCommonMap<SesComplaint>
    {
        public SesComplaintMap()
        {
            Table("ses_notifications.complaints");
            MapCommon();
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.ComplaintSubType).Column("complaint_sub_type");
            Map(x => x.ComplaintFeedbackType).Column("complaint_feedback_type");
            Map(x => x.FeedbackId).Column("feedback_id");
            Map(x => x.ComplainedRecipients).Column("complained_recipients");
            Map(x => x.UserAgent).Column("user_agent");
            Map(x => x.ArrivalDate).Column("arrival_date");
        }
    }
}