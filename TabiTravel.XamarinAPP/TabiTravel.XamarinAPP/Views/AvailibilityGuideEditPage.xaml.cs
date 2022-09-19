using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvailibilityGuideEditPage : ContentPage
    {
        private AvailabilityGuideVM AvailabilityGuideVM;

        public AvailibilityGuideEditPage()
        {
            InitializeComponent();
            AvailabilityGuideVM = new AvailabilityGuideVM(Navigation, this);
            BindingContext = AvailabilityGuideVM;
            UnavailibilityArrow.RelRotateTo(90);
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                await AvailabilityGuideVM?.GetAvailabilityGuideListByUserId();
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            PageContent.IsVisible = true;
            Loader.IsVisible = false;
        }

        private async void ShowUnavailibility_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UnavailibilityGuideListPage());
        }
    }
}