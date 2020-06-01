using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesSendEventMap : SesCommonMap<SesSendEvent>
    {
        public SesSendEventMap()
        {
            Table("ses_notifications.sendevents");
            MapCommon();
            Map(x => x.Recipients).Column("recipients");
        }
    }
}