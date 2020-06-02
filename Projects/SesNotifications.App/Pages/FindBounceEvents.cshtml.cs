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
    public class FindBounceEventsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesBounceEvent> BounceEvents { get; set; } = new List<SesBounceEvent>();

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

        public FindBounceEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            BounceEvents = _searchService.FindBounceEvents(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());
            return Page();
        }
    }
}