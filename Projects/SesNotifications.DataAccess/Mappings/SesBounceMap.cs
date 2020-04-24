using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesBounceMap : SesCommonMap<SesBounce>
    {
        public SesBounceMap()
        {
            Table("ses_notifications.bounces");
            MapCommon();
            Map(x => x.BounceType).Column("bounce_type");
            Map(x => x.BounceSubType).Column("bounce_sub_type");
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.FeedbackId).Column("feedback_id");
            Map(x => x.ReportingMta).Column("reporting_mta");
            Map(x => x.RemoteMtaIp).Column("remote_mta_ip");
            Map(x => x.BouncedRecipients).Column("bounced_recipients");
        }
    }
}