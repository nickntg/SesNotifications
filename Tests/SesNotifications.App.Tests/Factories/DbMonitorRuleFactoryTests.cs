using SesNotifications.App.Factories;
using SesNotifications.App.Models;
using Xunit;

namespace SesNotifications.App.Tests.Factories
{
    public class DbMonitorRuleFactoryTests
    {
        [Fact]
        public void VerifyOneWay()
        {
            var rule = new MonitorRule
            {
                JsonMatcher = "json",
                SesMessage = "ses",
                Regex = "regex",
                Name = "name",
                Id = 1
            };

            var created = rule.Create();

            Assert.Equal(created.JsonMatcher, rule.JsonMatcher);
            Assert.Equal(created.Regex, rule.Regex);
            Assert.Equal(created.SesMessage, rule.SesMessage);
            Assert.Equal(created.Name, rule.Name);
            Assert.Equal(created.Id, rule.Id);
        }

        [Fact]
        public void VerifyOtherWay()
        {
            var rule = new DataAccess.Entities.MonitorRule()
            {
                JsonMatcher = "json",
                SesMessage = "ses",
                Regex = "regex",
                Name = "name",
                Id = 1
            };

            var created = rule.Create();

            Assert.Equal(created.JsonMatcher, rule.JsonMatcher);
            Assert.Equal(created.Regex, rule.Regex);
            Assert.Equal(created.SesMessage, rule.SesMessage);
            Assert.Equal(created.Name, rule.Name);
            Assert.Equal(created.Id, rule.Id);
        }
    }
}
