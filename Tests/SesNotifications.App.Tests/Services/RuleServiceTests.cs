using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using SesNotifications.App.Models;
using SesNotifications.App.Services;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;
using SesNotifications.DataAccess.Repositories.Interfaces;
using Xunit;
using MonitorRule = SesNotifications.DataAccess.Entities.MonitorRule;

namespace SesNotifications.App.Tests.Services
{
    public class RuleServiceTests
    {
        [Fact]
        public void NothingHappensWithoutConfiguration()
        {
            var mockConfigRepo = new Mock<IConfigurationRepository>(MockBehavior.Strict);
            mockConfigRepo.Setup(x => x.GetByKey("sqs_notification_config")).Returns((ConfigurationItem) null);

            var service = new RuleService(mockConfigRepo.Object, null, null);

            service.ProcessMessage("json", MonitorRuleType.BounceEvent);

            mockConfigRepo.Verify(x => x.GetByKey("sqs_notification_config"), Times.Once);
        }

        [Fact]
        public void NothingHappensWithoutConfiguredRule()
        {
            var mockConfigRepo = GetConfigRepository();
            var mockRulesRepo = GetRuleRepository(new List<MonitorRule> { new MonitorRule { JsonMatcher = "matcher", SesMessage = SesMessageTypes.SendEvent.ToString()}});

            var service = new RuleService(mockConfigRepo.Object, mockRulesRepo.Object, null);

            service.ProcessMessage("invalid json", MonitorRuleType.BounceEvent);

            mockConfigRepo.Verify(x => x.GetByKey("sqs_notification_config"), Times.Once);
            mockRulesRepo.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public void NothingHappensWithoutMatch()
        {
            var mockConfigRepo = GetConfigRepository();
            var mockRulesRepo = GetRuleRepository(new List<MonitorRule> { new MonitorRule { JsonMatcher = "$.field", Regex = "non_existent", SesMessage = SesMessageTypes.BounceEvent.ToString() } });

            var service = new RuleService(mockConfigRepo.Object, mockRulesRepo.Object, null);

            service.ProcessMessage(Json, MonitorRuleType.BounceEvent);

            mockConfigRepo.Verify(x => x.GetByKey("sqs_notification_config"), Times.Once);
            mockRulesRepo.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public void NotificationSend()
        {
            var mockConfigRepo = GetConfigRepository();
            var mockRulesRepo = GetRuleRepository(new List<MonitorRule> { new MonitorRule { JsonMatcher = "$.field", Regex = "value", SesMessage = SesMessageTypes.BounceEvent.ToString() } });
            var mockNotifier = new Mock<ISqsNotifier>(MockBehavior.Strict);
            mockNotifier.Setup(x => x.Notify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SqsConfiguration>()));

            var service = new RuleService(mockConfigRepo.Object, mockRulesRepo.Object, mockNotifier.Object);

            service.ProcessMessage(Json, MonitorRuleType.BounceEvent);

            mockConfigRepo.Verify(x => x.GetByKey("sqs_notification_config"), Times.Once);
            mockRulesRepo.Verify(x => x.GetAll(), Times.Once());
            mockNotifier.Verify(x => x.Notify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SqsConfiguration>()), Times.Once);
        }

        private Mock<IMonitorRuleRepository> GetRuleRepository(IList<MonitorRule> rules)
        {
            var repo = new Mock<IMonitorRuleRepository>(MockBehavior.Strict);
            repo.Setup(x => x.GetAll()).Returns(rules);
            return repo;
        }

        private Mock<IConfigurationRepository> GetConfigRepository()
        {
            var mockConfigRepo = new Mock<IConfigurationRepository>(MockBehavior.Strict);
            mockConfigRepo.Setup(x => x.GetByKey("sqs_notification_config")).Returns(new ConfigurationItem { Value = JsonConvert.SerializeObject(new SqsConfiguration())});
            return mockConfigRepo;
        }

        private const string Json = "{\"field\": \"value\"}";
    }
}
