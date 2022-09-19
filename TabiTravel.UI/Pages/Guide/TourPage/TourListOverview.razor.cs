using Microsoft.AspNetCore.Components;
using TabiTravel.Data.VM.TourVM;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.UI.Pages.Guide.InterestPage;

namespace TabiTravel.UI.Pages.Guide.TourPage
{
    public partial class TourListOverview
    {
        [Inject] ITourService TourService { get; set; }
        [Inject] protected Sotsera.Blazor.Toaster.IToaster Toaster { get; set; }
        public List<GetMyTourListVM> MyTourVMs { get; set; } = new();
        public List<GetMyTourListVM> MyTourVMsCopy { get; set; } = new();
        public FilterGuidePage FilterGuidePageItem { get; set; } = new();
        
        private string _message = "No item(s)";
        private bool Loading { get; set; }

        protected bool orderAscending = true;
        protected string LanguageIso { get; set; } = string.Empty;
        protected List<string> Filters { get; set; } = new() { "Category", "City", "Country", "Language", "Name" };

        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            await LoadList();
            Loading = false;
            MyTourVMsCopy = MyTourVMs;
        }

        #region LoadList
        protected async Task LoadList()
        {
            Guid userid = Guid.Parse("2046249e-1ab1-4c5f-8a08-42e1c9f07542");
            LanguageIso = "EN";

            if(MyTourVMsCopy.Count == 0)
            {
                MyTourVMs = (await TourService.GetMyTourVMs(userid, LanguageIso)).OrderBy(x => x.Name).ToList();
            }
            else
            {
                MyTourVMs = MyTourVMsCopy;
            }

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
                        MyTourVMs = MyTourVMs.OrderBy(c => c.Name).ToList();
                        break;
                    case "city":
                        MyTourVMs = MyTourVMs.OrderBy(c => c.City).ToList();
                        break;
                    case "country":
                        MyTourVMs = MyTourVMs.OrderBy(c => c.Country).ToList();
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "name":
                        MyTourVMs = MyTourVMs.OrderByDescending(c => c.Name).ToList();
                        break;
                    case "city":
                        MyTourVMs = MyTourVMs.OrderByDescending(c => c.City).ToList();
                        break;
                    case "country":
                        MyTourVMs = MyTourVMs.OrderByDescending(c => c.Country).ToList();
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
                        MyTourVMs = MyTourVMsCopy.Where(x => x.Name.ToLower().Contains(name) && x.Name != null).ToList();
                    }
                    break;
                case "Country":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        MyTourVMs = (MyTourVMsCopy.Where(x => x.Country != null).ToList()).Where(x => x.Country.ToLower().Contains(name)).ToList();
                    }
                    break;
                case "City":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        MyTourVMs = (MyTourVMsCopy.Where(x => x.City != null).ToList()).Where(x => x.City.ToLower().Contains(name)).ToList();
                    }
                    break;
                case "Language":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        MyTourVMs = MyTourVMsCopy.Where(x => x.Languages.Contains(name.ToUpper()) && x.Languages != null).ToList();
                    }
                    break;
                case "Category":
                    if (!(String.IsNullOrWhiteSpace(name)))
                    {
                        MyTourVMs = MyTourVMsCopy.Where(x => x.Categories.Contains(name) && x.Categories != null).ToList();
                    }
                    break;
            }
        }
        #endregion

        #region Delete
        protected async void Delete(int id)
        {
            //Guid temporaire à changer plus tard
            Guid userid = Guid.Parse("ff9cadb0-f2c7-4d20-b039-64bdd330fce3");

            var res = await TourService.Delete(id, userid);
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
