using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindOpenEventsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesOpenEvent> OpenEvents { get; set; } = new List<SesOpenEvent>();

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

        public FindOpenEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            OpenEvents = _searchService.FindOpenEvents(Input.Email, Input.Start, Input.End);
            return Page();
        }
    }
}