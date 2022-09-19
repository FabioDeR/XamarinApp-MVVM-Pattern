using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.BookingVM;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Guide.InterestPage;

namespace TabiTravel.UI.Pages.Tourist.BookingPage
{
    public partial class BookingListOverview
    {
        [Inject] IBookingService BookingService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }


        public FilterGuidePage FilterGuidePageItem { get; set; } = new();
        public List<BookingListVM> BookingListVMs { get; set; } = new();
        public List<BookingListVM> BookingListVMsCopy { get; set; } = new();

        private bool Loading { get; set; }
        protected bool orderAscending = true;
        protected string LanguageIso { get; set; } = string.Empty;
        protected List<string> Filters { get; set; } = new() { "Date End", "Date Start", "City", "Name", "Status : Accepted", "Status : In waiting", "Status : Refused" };
        private string _message = "No item(s)";

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            BookingListVMsCopy = BookingListVMs;
        }

        #region LoadList
        protected async Task LoadList()
        {
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            LanguageIso = "EN";
            BookingListVMs = await BookingService.GetBookingListVMGuide(userid, LanguageIso);
            FilterGuidePageItem.Name = string.Empty;
            FilterGuidePageItem.FilterName = "No";
            StateHasChanged();
        }
        #endregion


        #region OrderBy
        protected void SortBy(string type)
        {
            orderAscending = !orderAscending;

            if (orderAscending)
            {
                switch (type)
                {
                    case "name":
                        BookingListVMs = BookingListVMs.OrderBy(c => c.TourName).ToList();
                        break;
                    case "city":
                        BookingListVMs = BookingListVMs.OrderBy(c => c.TourCity).ToList();
                        break;
                    case "country":
                        BookingListVMs = BookingListVMs.OrderBy(c => c.TourCountry).ToList();
                        break;
                    case "start":
                        BookingListVMs = BookingListVMs.OrderBy(c => c.DateStart).ToList();
                        break;
                    case "end":
                        BookingListVMs = BookingListVMs.OrderBy(c => c.DateEnd).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "name":
                        BookingListVMs = BookingListVMs.OrderByDescending(c => c.TourName).ToList();
                        break;
                    case "city":
                        BookingListVMs = BookingListVMs.OrderByDescending(c => c.TourCity).ToList();
                        break;
                    case "country":
                        BookingListVMs = BookingListVMs.OrderByDescending(c => c.TourCountry).ToList();
                        break;
                    case "start":
                        BookingListVMs = BookingListVMs.OrderByDescending(c => c.DateStart).ToList();
                        break;
                    case "end":
                        BookingListVMs = BookingListVMs.OrderByDescending(c => c.DateEnd).ToList();
                        break;
                }
            }
            StateHasChanged();
        }
        #endregion

        #region Filter
        protected void Filter()
        {
            string name = FilterGuidePageItem.Name.ToLower();
            string filter = FilterGuidePageItem.FilterName;

            switch (filter)
            {
                case "Name":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {

                    }
                    break;
                case "Country":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        BookingListVMs = (BookingListVMsCopy.Where(x => x.TourCountry != null).ToList()).Where(x => x.TourCountry.ToLower().Contains(name)).ToList();
                    }
                    break;
                case "City":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        BookingListVMs = (BookingListVMsCopy.Where(x => x.TourCity != null).ToList()).Where(x => x.TourCity.ToLower().Contains(name)).ToList();
                    }
                    break;
                case "Date Start":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        BookingListVMs = BookingListVMsCopy.Where(x => x.DateStart.ToShortDateString().Contains(name)).ToList();
                    }
                    break;
                case "Date End":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        BookingListVMs = BookingListVMsCopy.Where(x => x.DateEnd.ToShortDateString().Contains(name)).ToList();
                    }
                    break;
                case "Status : Accepted":
                    BookingListVMs = BookingListVMsCopy.Where(x => x.IsAccepted).ToList();
                    break;
                case "Status : Refused":
                    BookingListVMs = BookingListVMsCopy.Where(x => x.IsRefused).ToList();
                    break;
                case "Status : In waiting":
                    BookingListVMs = BookingListVMsCopy.Where(x => x.IsWaiting).ToList();
                    break;
            }

        }
        #endregion

        #region Delete
        protected async void Delete(int id)
        {
            //Guid temporaire à changer plus tard
            Guid userid = Guid.Parse("ff9cadb0-f2c7-4d20-b039-64bdd330fce3");

            var res = await BookingService.Delete(id, userid);
            if (res.IsSuccessStatusCode)
            {
                Toaster.Success("Item deleted");
                await LoadList();
            }
            else
            {
                Toaster.Warning("Error during delete");
            }
        }
        #endregion
    }
}
