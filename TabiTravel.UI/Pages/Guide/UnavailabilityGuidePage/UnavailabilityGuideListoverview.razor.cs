using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.UnavailabilityGuide;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Guide.InterestPage;

namespace TabiTravel.UI.Pages.Guide.UnavailabilityGuidePage
{
    public partial class UnavailabilityGuideListoverview
    {
        [Inject] IUnavailabilityGuideService UnavailabilityGuideService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public FilterGuidePage FilterGuidePageItem { get; set; } = new();

        public List<UnavailabilityGuideEditVM> UnavailabilityGuideVMs { get; set; } = new();
        public List<UnavailabilityGuideEditVM> UnavailabilityGuideVMsCopy { get; set; } = new();

        private bool Loading { get; set; }
        protected bool orderAscending = true;
        protected string LanguageIso { get; set; } = string.Empty;
        protected List<string> Filters { get; set; } = new() { "Date End", "Date Start"};
        private string _message = "No item(s)";

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            UnavailabilityGuideVMsCopy = UnavailabilityGuideVMs;
        }

        #region LoadList
        protected async Task LoadList()
        {
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            LanguageIso = "EN";
            UnavailabilityGuideVMs = await UnavailabilityGuideService.GetUnavailabilityGuideVM(userid);
            FilterGuidePageItem.Name = string.Empty;
            FilterGuidePageItem.FilterName = "No";
            FilterGuidePageItem.Date = DateTime.Now;
            StateHasChanged();
        }
        #endregion

        #region Delete
        protected async void Delete(int id)
        {
            //Guid temporaire à changer plus tard
            Guid userid = Guid.Parse("ff9cadb0-f2c7-4d20-b039-64bdd330fce3");

            var res = await UnavailabilityGuideService.Delete(id, userid);
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

        #region OrderBy
        protected void SortBy(string type)
        {
            orderAscending = !orderAscending;

            if (orderAscending)
            {
                switch (type)
                {
                    case "start":
                        UnavailabilityGuideVMs = UnavailabilityGuideVMs.OrderBy(c => c.DateStart).ToList();
                        break;
                    case "end":
                        UnavailabilityGuideVMs = UnavailabilityGuideVMs.OrderBy(c => c.DateEnd).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "start":
                        UnavailabilityGuideVMs = UnavailabilityGuideVMs.OrderByDescending(c => c.DateStart).ToList();
                        break;
                    case "end":
                        UnavailabilityGuideVMs = UnavailabilityGuideVMs.OrderByDescending(c => c.DateEnd).ToList();
                        break;
                }
            }
            StateHasChanged();
        }
        #endregion

        #region Filter
        protected void Filter()
        {
            DateTime date = FilterGuidePageItem.Date;
            string filter = FilterGuidePageItem.FilterName;

            switch (filter)
            {
                case "Date End":
                    if (!(String.IsNullOrWhiteSpace(date.ToShortDateString())))
                    {
                        UnavailabilityGuideVMs = (UnavailabilityGuideVMsCopy.Where(x => x.DateEnd.Date == date.Date).ToList()).ToList();
                    }
                    break;
                case "Date Start":
                    if (!(String.IsNullOrWhiteSpace(date.ToShortDateString())))
                    {
                        UnavailabilityGuideVMs = (UnavailabilityGuideVMsCopy.Where(x => x.DateStart.Date == date.Date).ToList()).ToList();
                    }
                    break;
            }

        }
        #endregion
    }
}
