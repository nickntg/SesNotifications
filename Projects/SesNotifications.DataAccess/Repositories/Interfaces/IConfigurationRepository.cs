using SesNotifications.DataAccess.Entities;

namespace SesNotifications.DataAccess.Repositories.Interfaces
{
    public interface IConfigurationRepository
    {
        ConfigurationItem GetByKey(string key);
    }
}