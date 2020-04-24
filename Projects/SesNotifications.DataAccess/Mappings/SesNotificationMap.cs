using FluentNHibernate.Mapping;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesNotificationMap : ClassMap<SesNotification>
    {
        public SesNotificationMap()
        {
            Table("notifications");
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.Notification).Column("notification");
            Map(x => x.ReceivedAt).Column("received_at");
            Map(x => x.MessageId).Column("message_id");
            Map(x => x.SentAt).Column("sent_at");
        }
    }
}