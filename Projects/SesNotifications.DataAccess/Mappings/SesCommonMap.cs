using FluentNHibernate.Mapping;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class SesCommonMap<T> : ClassMap<T> where T: SesCommon
    {
        public void MapCommon()
        {
            Id(x => x.Id).GeneratedBy.Identity().Column("id");
            Map(x => x.NotificationId).Column("notification_id");
            Map(x => x.NotificationType).Column("notification_type");
            Map(x => x.SentAt).Column("sent_at");
            Map(x => x.MessageId).Column("message_id");
            Map(x => x.Source).Column("source");
            Map(x => x.SourceArn).Column("source_arn");
            Map(x => x.SourceIp).Column("source_ip");
            Map(x => x.SendingAccountId).Column("sending_account_id");
        }
    }
}
