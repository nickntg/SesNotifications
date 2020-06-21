using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Factories;
using SesNotifications.App.Models;
using SesNotifications.DataAccess.Repositories.Interfaces;

namespace SesNotifications.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MonitorRuleController : ControllerBase
    {
        private readonly IMonitorRuleRepository _monitorRuleRepository;

        public MonitorRuleController(IMonitorRuleRepository monitorRuleRepository)
        {
            _monitorRuleRepository = monitorRuleRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var rule = _monitorRuleRepository.Get(id);
            if (rule == null)
            {
                return NotFound();
            }

            return Ok(rule.Create());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rules = _monitorRuleRepository.GetAll();
            return Ok(rules.Select(item => item.Create()).ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] MonitorRule rule)
        {
            if (rule == null ||
                string.IsNullOrEmpty(rule.Regex) ||
                string.IsNullOrEmpty(rule.JsonMatcher) ||
                string.IsNullOrEmpty(rule.Name))
            {
                return BadRequest("Invalid rule specified");
            }


            if (!Enum.TryParse<SesMessageTypes>(rule.SesMessage, out _))
            {
                return BadRequest("Invalid ses message");
            }

            var dbRule = rule.Create();
            _monitorRuleRepository.Save(dbRule);

            return Ok(dbRule.Create());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var rule = _monitorRuleRepository.Get(id);
            if (rule == null)
            {
                return NotFound();
            }

            _monitorRuleRepository.Delete(rule);

            return Ok();
        }
    }
}