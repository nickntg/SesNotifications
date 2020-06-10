using FluentNHibernate.Mapping;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesOperationalMap : ClassMap<SesOperational>
    {
        public SesOperationalMap()
        {
            Table("ses_notifications.operational");
            Id(x => x.NotificationId).Column("notification_id");
            Map(x => x.NotificationType).Column("notification_type");
            Map(x => x.Recipients).Column("recipients");
            Map(x => x.CreatedAt).Column("created_at");
            Map(x => x.SentAt).Column("sent_at");
            Map(x => x.Source).Column("source");
            Map(x => x.Detail1).Column("detail1");
            Map(x => x.Detail2).Column("detail2");
            ReadOnly();
        }
    }
}