using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.TourModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TourListPage : ContentPage
    {
        protected bool IsLoadListTour { get; set; }
        TourListVM TourListVM;
        public TourListPage()
        {
            InitializeComponent();
            TourListVM = new TourListVM(Navigation, this);
            BindingContext = TourListVM;
            IsLoadListTour = true;
        }

        public TourListPage(SearchVM searchVM)
        {
            InitializeComponent();
            TourListVM = new TourListVM(Navigation, this);
            BindingContext = TourListVM;
            TourListVM?.GetSearchTour(searchVM);
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                if (IsLoadListTour)
                {
                    await TourListVM.LoadTourListByUserId();
                    if (TourListVM.GetMyTourListVMs.Count() == 0)
                    {
                        StackButtonAdd.IsVisible = true;
                    }
                    else
                    {
                        StackButtonAdd.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            base.OnAppearing();
            PageContent.IsVisible = true;
            Loader.IsVisible = false;
        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(EnumSearch.Tour), true);
        }
    }
}