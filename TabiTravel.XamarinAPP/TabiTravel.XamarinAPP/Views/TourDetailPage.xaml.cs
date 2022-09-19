using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TourDetailPage : ContentPage
    {
        TourOverviewVM TourOverviewVM;
        public int TourId = new int();
        private bool IsGuide { get; set; } = false;
        public TourDetailPage(int tourId, bool? isGuid = null)
        {
            InitializeComponent();
            TourOverviewVM = new TourOverviewVM(Navigation, this);
            BindingContext = TourOverviewVM;
            TourId = tourId;
            IsGuide = (bool)isGuid;
        }
        protected async override void OnAppearing()
        {
            try
            {
                Loader.IsVisible = true;
                await TourOverviewVM.LoadTourOverview(TourId);
                if (IsGuide)
                {
                    ButtonBooking.IsVisible = false;
                }
                PageContent.IsVisible = true;
                Loader.IsVisible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;              

            }
            if (TourOverviewVM.StepListOverviewVM.PricePerAdult == null && TourOverviewVM.StepListOverviewVM.PricePerChild == null && TourOverviewVM.StepListOverviewVM.PricePerVehicule == null)
            {
                PricingLayout.IsVisible = false;
            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MoreMenuPage(this, TourId, NavigationEnum.TourEdit,EnumTranslate.Tour), true);
        }

        private void Button_Booking_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PreBookingPage(TourId));
        }
    }
}