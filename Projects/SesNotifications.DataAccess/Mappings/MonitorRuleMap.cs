using FluentNHibernate.Mapping;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class MonitorRuleMap : ClassMap<MonitorRule>
    {
        public MonitorRuleMap()
        {
            Table("ses_notifications.monitorrules");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.JsonMatcher).Column("json_matcher");
            Map(x => x.Name).Column("name");
            Map(x => x.Regex).Column("regex");
            Map(x => x.SesMessage).Column("ses_message");
            Cache.ReadWrite();
        }
    }
}