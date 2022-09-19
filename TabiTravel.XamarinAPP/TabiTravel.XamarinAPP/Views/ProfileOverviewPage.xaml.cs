using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileOverviewPage : ContentPage
    {
        UserVM userVM;

        public ProfileOverviewPage()
        {
            InitializeComponent();
            userVM = new UserVM(Navigation, this);
            BindingContext = userVM;
        }
        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                await userVM.GetUserById();
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
            await Navigation.PushAsync(new ProfileEditPage());
        }
    }
}