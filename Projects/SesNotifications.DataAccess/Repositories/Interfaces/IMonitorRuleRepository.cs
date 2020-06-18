using System.Collections.Generic;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface IMonitorRuleRepository
    {
        void Save(MonitorRule rule);
        void Delete(MonitorRule rule);
        MonitorRule Get(int id);
        IList<MonitorRule> GetAll();
    }
}