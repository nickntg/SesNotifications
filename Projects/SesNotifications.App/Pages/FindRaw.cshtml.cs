using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindRawModel : PageBase
    {
        [BindProperty]
        public RawInputModel Input { get; set; }

        public IList<SesNotification> Raw { get; set; } = new List<SesNotification>();

        private readonly ISearchService _searchService;

        public FindRawModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        protected override void Search()
        {
            var countOfResults = _searchService.FindRawCount(Input.Start.StartOfDay(), Input.End.EndOfDay());

            Raw = _searchService.FindRaw(Input.Start.StartOfDay(), Input.End.EndOfDay(), null, 0, PageSize);

            if (Raw.Count > 0)
            {
                FirstId = (int)Raw[0].Id;
            }

            PageNumber = 1;
            NumberOfPages = countOfResults / PageSize + 1;
            Start = Input.Start;
            End = Input.End;
        }

        protected override void GetPage()
        {
            Raw = _searchService.FindRaw(Start.StartOfDay(),
                End.EndOfDay(),
                FirstId,
                PageNumber - 1,
                PageSize);
        }

        protected override void CreateModel()
        {
            Input = new RawInputModel();
        }

        protected override void SaveState()
        {
            SaveState(Input);
        }
    }

    public class RawInputModel : BaseInputModel
    {

    }
}