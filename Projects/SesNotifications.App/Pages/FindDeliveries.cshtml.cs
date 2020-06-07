using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindDeliveriesModel : PageBase
    {
        [BindProperty]
        public BaseEmailInputModel Input { get; set; }

        public IList<SesDelivery> Deliveries { get; set; } = new List<SesDelivery>();

        private readonly ISearchService _searchService;

        public FindDeliveriesModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        protected override void Search()
        {
            var countOfResults = _searchService.FindDeliveriesCount(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());

            Deliveries = _searchService.FindDeliveries(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay(), null, 0, PageSize);

            if (Deliveries.Count > 0)
            {
                FirstId = (int)Deliveries[0].Id;
            }

            PageNumber = 1;
            NumberOfPages = countOfResults / PageSize + 1;
            Start = Input.Start;
            End = Input.End;
            Email = Input.Email;
        }

        protected override void GetPage()
        {
            Deliveries = _searchService.FindDeliveries(
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