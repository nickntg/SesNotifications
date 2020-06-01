using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindSendEventsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesSendEvent> SendEvents { get; set; } = new List<SesSendEvent>();

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

        public FindSendEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            SendEvents = _searchService.FindSendEvents(Input.Email, Input.Start, Input.End);
            return Page();
        }
    }
}