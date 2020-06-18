using System.Collections.Generic;
using NHibernate;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.DataAccess.Repositories
{
    public class MonitorRuleRepository : Repository, IMonitorRuleRepository
    {
        public MonitorRuleRepository()
        {
        }

        public MonitorRuleRepository(ISession session) : base(session)
        {
        }

        public void Save(MonitorRule rule)
        {
            Session.Save(rule);
        }

        public void Delete(MonitorRule rule)
        {
            Session.Delete(rule);
        }

        public MonitorRule Get(int id)
        {
            return Session.Get<MonitorRule>(id);
        }

        public IList<MonitorRule> GetAll()
        {
            return Session.CreateCriteria<MonitorRule>()
                .SetCacheable(true)
                .List<MonitorRule>();
        }
    }
}