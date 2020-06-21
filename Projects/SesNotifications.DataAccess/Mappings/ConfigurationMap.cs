using FluentNHibernate.Mapping;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Mappings
{
    public class ConfigurationMap : ClassMap<ConfigurationItem>
    {
        public ConfigurationMap()
        {
            Table("ses_notifications.configuration");
            Id(x => x.Id).Column("id").GeneratedBy.Identity();
            Map(x => x.Key).Column("key");
            Map(x => x.Value).Column("value");
            Cache.ReadOnly();
        }
    }
}