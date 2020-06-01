using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesSendMap : SesCommonMap<SesSend>
    {
        public SesSendMap()
        {
            Table("ses_notifications.sends");
            MapCommon();
            Map(x => x.Recipients).Column("recipients");
        }
    }
}