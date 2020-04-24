using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesDeliveryMap : SesCommonMap<SesDelivery>
    {
        public SesDeliveryMap()
        {
            Table("deliveries");
            MapCommon();
            Map(x => x.DeliveredAt).Column("delivered_at");
            Map(x => x.SmtpResponse).Column("smtp_response");
            Map(x => x.ReportingMta).Column("reporting_mta");
            Map(x => x.RemoteMtaIp).Column("remote_mta_ip");
            Map(x => x.Recipients).Column("recipients");
        }
    }
}