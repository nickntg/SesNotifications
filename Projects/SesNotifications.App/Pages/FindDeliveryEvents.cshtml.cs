using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindDeliveryEventsModel : PageBase
    {
        [BindProperty]
        public BaseEmailInputModel Input { get; set; }

        public IList<SesDeliveryEvent> DeliveryEvents { get; set; } = new List<SesDeliveryEvent>();

        private readonly ISearchService _searchService;

        public FindDeliveryEventsModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        protected override void Search()
        {
            var countOfResults = _searchService.FindDeliveryEventsCount(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());

            DeliveryEvents = _searchService.FindDeliveryEvents(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay(), null, 0, PageSize);

            if (DeliveryEvents.Count > 0)
            {
                FirstId = (int)DeliveryEvents[0].Id;
            }

            PageNumber = 1;
            NumberOfPages = countOfResults / PageSize + 1;
            Start = Input.Start;
            End = Input.End;
            Email = Input.Email;
        }

        protected override void GetPage()
        {
            DeliveryEvents = _searchService.FindDeliveryEvents(
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