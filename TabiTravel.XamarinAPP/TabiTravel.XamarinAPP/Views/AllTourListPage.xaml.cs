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
    public partial class AllTourListPage : ContentPage
    {
        protected bool IsLoadListTour { get; set; }
        TourListVM TourListVM;
        public AllTourListPage()
        {
            InitializeComponent();
            TourListVM = new TourListVM(Navigation, this);
            BindingContext = TourListVM;
            IsLoadListTour = true;

        }
        public AllTourListPage(SearchVM searchVM)
        {
            InitializeComponent();
            TourListVM = new TourListVM(Navigation, this);
            BindingContext = TourListVM;

            Loader.IsVisible = true;
            try
            {
                TourListVM?.GetSearchTour(searchVM);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
            PageContent.IsVisible = true;
            Loader.IsVisible = false;
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            if (IsLoadListTour)
            {
                try
                {
                   await TourListVM?.LoadAllTourList();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    throw;
                }
            }
            PageContent.IsVisible = true;
            Loader.IsVisible = false;

            base.OnAppearing();
        }

        //private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        //{
        //    try
        //    {
        //        var itemSelected = e.SelectedItem as GetMyTourListVM;
        //        Navigation.PushAsync(new TourDetailPage(itemSelected.TourId, false));
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage(EnumSearch.AllTour), true);
        }
    }
}