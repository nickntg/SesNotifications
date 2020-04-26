using System;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Services.Interfaces;

namespace SesNotifications.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchesController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchesController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        [Route("{deliveries}")]
        public IActionResult FindDeliveries([FromQuery] string email, DateTime start, DateTime end)
        {
            return Ok(_searchService.FindDeliveries(email, start, end));
        }

        [HttpGet]
        [Route("{bounces}")]
        public IActionResult FindBounces([FromQuery] string email, DateTime start, DateTime end)
        {
            return Ok(_searchService.FindBounces(email, start, end));
        }

        [HttpGet]
        [Route("{complaints}")]
        public IActionResult FindComplaints([FromQuery] string email, DateTime start, DateTime end)
        {
            return Ok(_searchService.FindComplaints(email, start, end));
        }

        [HttpGet]
        [Route("{raw}")]
        public IActionResult FindRaw([FromQuery] DateTime start, DateTime end)
        {
            return Ok(_searchService.FindRaw(start, end));
        }
    }
}