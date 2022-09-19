using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using TabiTravel.Data.DbModel;
using TabiTravel.Data.VM.CategoryVM;
using TabiTravel.Data.VM.InterestVM;
using TabiTravel.Data.VM.ResearchVM;
using TabiTravel.Data.VM.TourVM;
using TabiTravel.ServiceUI.Interface;
using TabiTravel.Shared.Enum;

namespace TabiTravel.UI.Pages.Tourist.SearchPage
{
    public partial class Search
    {
        [Inject] public ISearchService SearchService { get; set; }
        [Inject] public ICategoryService CategoryService { get; set; }
        [Inject] ILanguageService LanguageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public SearchVM Item { get; set; } = new();
        public List<GetCategoryVM> Categories { get; set; } = new();
        public List<Language> Languages { get; set; } = new();

        public List<GetMyTourListVM>  MyTourList {get; set;} = new();
        public List<GetMyInterestVM> MyInterestList { get; set; } = new();
        public ResultSearchVM ResultSearchVM { get; set; } = new();
        public string languageiso { get; set; } = "EN";
        public bool IsShowFilters { get; set; } = true;

        private bool Loading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Item.enumSearch = (EnumSearch.Tour);
            Loading = true;
            await LoadList();
            Loading = false;
            Item.LanguageIso = languageiso;
        }

        #region LoadList
        protected async Task LoadList()
        {
            string languageiso = "EN";
            Languages = (await LanguageService.GetAll()).OrderBy(x => x.Name).ToList();

            //1 Interest    2= Tour
            if ((int)Item.enumSearch == 1)
            {
                Categories = (await CategoryService.GetAllCategoryVMForInterest(languageiso)).OrderBy(x => x.Name).ToList();
            }
            if ((int)Item.enumSearch > 1)
            {
                Categories = (await CategoryService.GetAllCategoryVMForTour(languageiso)).OrderBy(x => x.Name).ToList();
            }

            StateHasChanged();
        }
        #endregion
        #region Show Filter
        public async void ShowFilter()
        {
            if (IsShowFilters)
            {
                IsShowFilters = false;
            }
            else
            {
                IsShowFilters = true;
            }
        }
        #endregion

        #region GoToTourOverview
        public async void GoToTourOverview(int tourid)
        {
            NavigationManager.NavigateTo($"/tourist/touroverview/{tourid}");
        }
        #endregion

        #region GoToInterestOverview
        public async void GoToInterestOverview(int interestid)
        {
            NavigationManager.NavigateTo($"/tourist/interestoverview/{interestid}");
        }
        #endregion

        protected async Task HandleValidRequest()
        {
            var res = await SearchService.GetResultResearchVM(Item);
            if (res.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ResultSearchVM>(await res.Content.ReadAsStringAsync());
                MyTourList = result.getMyTourListVMs;
                MyInterestList = result.getMyInterestVMs;
            }  
        }
    }
}
