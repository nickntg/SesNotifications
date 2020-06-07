using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindBounceEventsModel : PageBase
    {
        [BindProperty]
        public BaseEmailInputModel Input { get; set; }

        public IList<SesBounceEvent> BounceEvents { get; set; } = new List<SesBounceEvent>();

        private readonly ISearchService _searchService;

        public FindBounceEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        protected override void Search()
        {
            var countOfResults = _searchService.FindBounceEventsCount(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());

            BounceEvents = _searchService.FindBounceEvents(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay(), null, 0, PageSize);

            if (BounceEvents.Count > 0)
            {
                FirstId = (int)BounceEvents[0].Id;
            }

            PageNumber = 1;
            NumberOfPages = countOfResults / PageSize + 1;
            Start = Input.Start;
            End = Input.End;
            Email = Input.Email;
        }

        protected override void GetPage()
        {
            BounceEvents = _searchService.FindBounceEvents(
                Email,
                Start.StartOfDay(),
                End.EndOfDay(),
                FirstId,
                PageNumber - 1,
                PageSize);
        }

        protected override void CreateModel()
        {
            Input = new BaseEmailInputModel();
        }

        protected override void SaveState()
        {
            SaveState(Input);
            Input.Email = Email;
        }
    }

    public class BaseEmailInputModel : BaseInputModel
    {
        public string Email { get; set; }
    }
}