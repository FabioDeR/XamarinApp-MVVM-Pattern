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
    public partial class UnavailibilityGuideListPage : ContentPage
    {
        public UnavailabilityGuideVM UnavailabilityGuideVM;

        protected Guid UserId { get; set; }
        public UnavailibilityGuideListPage()
        {
            InitializeComponent();
            UnavailabilityGuideVM = new UnavailabilityGuideVM(Navigation, this);
            BindingContext = UnavailabilityGuideVM;
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                await UnavailabilityGuideVM?.GetUnavailabilityGuideListByUserId();
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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UnavailibilityGuideEditPage());
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var itemSelected = e.SelectedItem as UnavailabilityGuideEditVM;                
                Navigation.PushAsync(new UnavailibilityGuideEditPage(itemSelected.UnavailabilityGuideId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}