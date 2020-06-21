using SesNotifications.App.Models;

namespace SesNotifications.App.Services.Interfaces
{
    public interface IRuleService
    {
        void ProcessMessage(string json, MonitorRuleType ruleType);
    }
}