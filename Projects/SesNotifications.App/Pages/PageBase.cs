using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SesNotifications.App.Pages
{
    public abstract class PageBase : PageModel
    {
        protected const int PageSize = 50;

        [TempData]
        public int FirstId { get; set; }

        [TempData]
        public int PageNumber { get; set; }

        [TempData]
        public int NumberOfPages { get; set; }

        [TempData]
        public DateTime Start { get; set; }

        [TempData]
        public DateTime End { get; set; }

        [TempData]
        public string Email { get; set; }

        protected PageBase()
        {
            FirstId = -1;
            PageNumber = 0;
        }

        public IActionResult OnPost()
        {
            Search();
            
            SaveState();

            TempData.Keep();

            return Page();
        }

        public IActionResult OnGet(string currentPage, string start, string end, string email)
        {
            if (!string.IsNullOrEmpty(currentPage))
            {
                var page = Convert.ToInt32(currentPage);

                PageNumber = page;
                Start = Convert.ToDateTime(start);
                End = Convert.ToDateTime(end);
                Email = email;
                GetPage();

                CreateModel();
                SaveState();

                TempData.Keep();
            }

            return Page();
        }

        protected abstract void SaveState();

        protected abstract void Search();

        protected abstract void GetPage();

        protected abstract void CreateModel();

        protected void SaveState(BaseInputModel input)
        {
            ViewData[nameof(PageNumber)] = PageNumber;
            ViewData[nameof(NumberOfPages)] = NumberOfPages;
            ViewData[nameof(Start)] = Start;
            ViewData[nameof(End)] = End;
            ViewData[nameof(Email)] = Email;

            input.End = End;
            input.Start = Start;
        }
    }

    public class BaseInputModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
    }
}
