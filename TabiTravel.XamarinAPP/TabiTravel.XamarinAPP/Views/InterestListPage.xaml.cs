using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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
    public partial class InterestListPage : ContentPage
    {
        public InterestListPageVM InterestListVM;
        public SearchPageVM searchPageVM;
        protected bool IsLoadListInterest { get; set; }
        protected bool IsLoadListInterestWithSearch { get; set; }
        protected SearchVM SearchVM;
        public InterestListPage()
        {
            InitializeComponent();
            InterestListVM = new InterestListPageVM(Navigation, this);
            BindingContext = InterestListVM;
            IsLoadListInterest = true;
            IsLoadListInterestWithSearch = false;

        }
        public InterestListPage(SearchVM searchVM)
        {
            InitializeComponent();
            InterestListVM = new InterestListPageVM(Navigation, this);
            BindingContext = InterestListVM;
            SearchVM = searchVM;
            IsLoadListInterest = false;
            IsLoadListInterestWithSearch = true;
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                if (IsLoadListInterest)
                {
                    await InterestListVM.GetAllInterestByUserId();
                    if (InterestListVM.Interests.Count() == 0)
                    {
                        StackButtonAdd.IsVisible = true;
                    }
                    else
                    {
                        StackButtonAdd.IsVisible = false;
                    }
                }
                if (IsLoadListInterestWithSearch)
                {
                    await InterestListVM.GetSearchInterest(SearchVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            PageContent.IsVisible = true;
            Loader.IsVisible = false;
            base.OnAppearing();

        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(EnumSearch.Interest), true);
        }
    }
}