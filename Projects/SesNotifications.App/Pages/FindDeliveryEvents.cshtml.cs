using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindDeliveryEventsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesDeliveryEvent> DeliveryEvents { get; set; } = new List<SesDeliveryEvent>();

        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Start { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime End { get; set; }

            public string Email { get; set; }
        }

        private readonly ISearchService _searchService;

        public FindDeliveryEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            DeliveryEvents = _searchService.FindDeliveryEvents(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());
            return Page();
        }
    }
}