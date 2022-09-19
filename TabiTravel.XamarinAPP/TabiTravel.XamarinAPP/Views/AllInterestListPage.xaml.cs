using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinAPP.ViewModels.POI;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllInterestListPage : ContentPage
    {
        public InterestListPageVM InterestListPageVM;
        public SearchPageVM searchPageVM;
        protected SearchVM SearchVM;
        protected bool IsLoadListInterest { get; set; }
        protected bool IsLoadListInterestWithSearch { get; set; }
        public AllInterestListPage()
        {
            InitializeComponent();
            InterestListPageVM = new InterestListPageVM(Navigation, this);
            BindingContext = InterestListPageVM;
            IsLoadListInterest = true;
            IsLoadListInterestWithSearch = false;
        }
        public AllInterestListPage(SearchVM searchVM)
        {
            InitializeComponent();
            InterestListPageVM = new InterestListPageVM(Navigation, this);
            BindingContext = InterestListPageVM;
            SearchVM = searchVM;
            IsLoadListInterestWithSearch = true;
            IsLoadListInterest = false;
        }
        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            if (IsLoadListInterest)
            {
                await InterestListPageVM?.GetAllInterest();
            }
            if (IsLoadListInterestWithSearch)
            {
                await InterestListPageVM?.GetSearchInterest(SearchVM);
            }
            PageContent.IsVisible = true;
            Loader.IsVisible = false;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(EnumSearch.AllInterest), true);
        }
    }
}