using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindRawModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesNotification> Raw { get; set; } = new List<SesNotification>();

        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Start { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime End { get; set; }
        }

        private readonly ISearchService _searchService;

        public FindRawModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            Raw = _searchService.FindRaw(Input.Start, Input.End);
            return Page();
        }
    }
}