using MonitorRule = SesNotifications.DataAccess.Entities.MonitorRule;

namespace SesNotifications.App.Factories
{
    public static class DbMonitorRuleFactory
    {
        public static MonitorRule Create(this Models.MonitorRule rule)
        {
            return new MonitorRule
            {
                SesMessage = rule.SesMessage,
                Name = rule.Name,
                JsonMatcher = rule.JsonMatcher,
                Regex = rule.Regex,
                Id = rule.Id
            };
        }

        public static Models.MonitorRule Create(this MonitorRule rule)
        {
            return new Models.MonitorRule
            {
                SesMessage = rule.SesMessage,
                Name = rule.Name,
                JsonMatcher = rule.JsonMatcher,
                Regex = rule.Regex,
                Id = rule.Id
            };
        }
    }
}