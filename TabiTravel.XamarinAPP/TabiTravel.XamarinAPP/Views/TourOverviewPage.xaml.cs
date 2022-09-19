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
    public partial class TourOverviewPage : ContentPage
    {
        TourOverviewVM TourOverviewVM;
        protected bool isLoadTour;
        int TourId { get; }
        public TourOverviewPage(int tourId)
        {
            InitializeComponent();
            TourOverviewVM = new TourOverviewVM(Navigation, this);
            BindingContext = TourOverviewVM;
            TourId = tourId;
            isLoadTour = true;
        }
        protected override async void OnAppearing()
        {
            try
            {
                if (isLoadTour)
                {
                    await TourOverviewVM?.LoadTourOverview(TourId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MoreMenuPage(this, TourId, NavigationEnum.TourOverviewEdit, EnumTranslate.Tour), true);
        }
    }
}