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
    public class FindComplaintEventsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesComplaintEvent> ComplaintEvents { get; set; } = new List<SesComplaintEvent>();

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

        public FindComplaintEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            ComplaintEvents = _searchService.FindComplaintEvents(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());
            return Page();
        }
    }
}