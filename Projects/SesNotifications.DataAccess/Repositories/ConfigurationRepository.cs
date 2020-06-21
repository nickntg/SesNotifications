using NHibernate;
using NHibernate.Criterion;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class ConfigurationRepository : Repository, IConfigurationRepository
    {
        public ConfigurationRepository()
        {
        }

        public ConfigurationRepository(ISession session) : base(session)
        {
        }

        public ConfigurationItem GetByKey(string key)
        {
            return Session.CreateCriteria<ConfigurationItem>()
                .Add(Restrictions.Eq(nameof(ConfigurationItem.Key), key))
                .SetCacheable(true)
                .UniqueResult<ConfigurationItem>();
        }
    }
}