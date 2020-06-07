using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindOpenEventsModel : PageBase
    {
        [BindProperty]
        public BaseEmailInputModel Input { get; set; }

        public IList<SesOpenEvent> OpenEvents { get; set; } = new List<SesOpenEvent>();

        private readonly ISearchService _searchService;

        public FindOpenEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        protected override void Search()
        {
            var countOfResults = _searchService.FindOpenEventsCount(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());

            OpenEvents = _searchService.FindOpenEvents(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay(), null, 0, PageSize);

            if (OpenEvents.Count > 0)
            {
                FirstId = (int)OpenEvents[0].Id;
            }

            PageNumber = 1;
            NumberOfPages = countOfResults / PageSize + 1;
            Start = Input.Start;
            End = Input.End;
            Email = Input.Email;
        }

        protected override void GetPage()
        {
            OpenEvents = _searchService.FindOpenEvents(
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
}