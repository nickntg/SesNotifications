using System;
using System.Linq;
using Newtonsoft.Json;
using NLog;
using SesNotifications.App.Helpers;
using SesNotifications.App.Models;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.App.Services
{
    public class RuleService : IRuleService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMonitorRuleRepository _monitorRuleRepository;
        private readonly ISqsNotifier _sqsNotifier;
        private readonly SqsConfiguration _sqsConfiguration;

        public RuleService(IConfigurationRepository configurationRepository,
            IMonitorRuleRepository monitorRuleRepository,
            ISqsNotifier sqsNotifier)
        {
            _monitorRuleRepository = monitorRuleRepository;
            _sqsNotifier = sqsNotifier;

            var sqsConfig = configurationRepository.GetByKey("sqs_notification_config");
            if (sqsConfig == null)
            {
                Logger.Debug("No SQS notification configuration found");
                return;
            }

            try
            {
                _sqsConfiguration = JsonConvert.DeserializeObject<SqsConfiguration>(sqsConfig.Value);
            }
            catch (Exception e)
            {
                Logger.Error(e, "SQS notification configuration is invalid");
                _sqsConfiguration = null;
            }
        }

        public void ProcessMessage(string json, MonitorRuleType ruleType)
        {
            if (_sqsConfiguration == null)
            {
                return;
            }

            var rules = _monitorRuleRepository.GetAll();
            var typeRules = rules.Where(x => x.SesMessage.ToLower() == ruleType.ToString().ToLower()).ToList();

            if (typeRules.Count == 0)
            {
                return;
            }

            var o = json.TokenizeJson();

            foreach (var rule in typeRules)
            {
                var extracted = o.FindToken(rule.JsonMatcher);
                if (extracted == null)
                {
                    break;
                }

                var isMatch = extracted.ToString().IsMatch(rule.Regex);

                if (isMatch)
                {
                    _sqsNotifier.Notify($"Rule {rule.Name} match", extracted.ToString(), _sqsConfiguration);
                }
            }
        }
    }
}