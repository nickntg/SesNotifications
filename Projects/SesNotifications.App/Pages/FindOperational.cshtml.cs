using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SesNotifications.App.Helpers;
using SesNotifications.App.Services.Interfaces;
using SesNotifications.DataAccess.Entities;

namespace SesNotifications.App.Pages
{
    public class FindOperationalModel : PageBase
    {
        [BindProperty]
        public BaseEmailInputModel Input { get; set; }

        public IList<SesOperational> Operational { get; set; } = new List<SesOperational>();

        private readonly ISearchService _searchService;

        public FindOperationalModel(ISearchService searchService)
        {
            _searchService = searchService;
        }

        protected override void Search()
        {
            var countOfResults = _searchService.FindOperationalCount(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay());

            Operational = _searchService.FindOperational(Input.Email, Input.Start.StartOfDay(), Input.End.EndOfDay(), null, 0, PageSize);

            if (Operational.Count > 0)
            {
                FirstId = (int)Operational[0].NotificationId;
            }

            PageNumber = 1;
            NumberOfPages = countOfResults / PageSize + 1;
            Start = Input.Start;
            End = Input.End;
            Email = Input.Email;
        }

        protected override void GetPage()
        {
            Operational = _searchService.FindOperational(
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