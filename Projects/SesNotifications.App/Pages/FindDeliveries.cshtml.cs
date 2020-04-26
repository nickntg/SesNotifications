using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindDeliveriesModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IList<SesDelivery> Deliveries { get; set; } = new List<SesDelivery>();

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

        public FindDeliveriesModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult OnPost()
        {
            Deliveries = _searchService.FindDeliveries(Input.Email, Input.Start, Input.End);
            return Page();
        }
    }
}