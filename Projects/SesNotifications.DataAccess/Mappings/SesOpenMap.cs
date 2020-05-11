using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesOpenMap : SesCommonMap<SesOpen>
    {
        public SesOpenMap()
        {
            Table("ses_notifications.opens");
            MapCommon();
            Map(x => x.Recipients).Column("recipients");
            Map(x => x.OpenedAt).Column("opened_at");
            Map(x => x.UserAgent).Column("user_agent");
            Map(x => x.IpAddress).Column("ip_address");
        }
    }
}