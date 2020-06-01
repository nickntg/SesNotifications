using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesDeliveryEventMap : SesCommonMap<SesDeliveryEvent>
    {
    public SesDeliveryEventMap()
    {
        Table("ses_notifications.deliveryevents");
        MapCommon();
        Map(x => x.DeliveredAt).Column("delivered_at");
        Map(x => x.SmtpResponse).Column("smtp_response");
        Map(x => x.ReportingMta).Column("reporting_mta");
        Map(x => x.Recipients).Column("recipients");
    }
    }
}