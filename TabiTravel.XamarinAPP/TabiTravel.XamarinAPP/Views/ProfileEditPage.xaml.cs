using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage : ContentPage
    {
        UserVM UserVM;
        public bool IsShowPersoInfo { get; set; }
        public bool IsShowBio { get; set; }
        public bool IsShowLocationInfo { get; set; }
        public bool IsShowAccountInfo { get; set; }
        public bool IsShowLanguage { get; set; }

        public ProfileEditPage()
        {
            InitializeComponent();
            UserVM = new UserVM(Navigation, this);
            BindingContext = UserVM;
            CountryArrow.RelRotateTo(90);
            AvailibilityArrow.RelRotateTo(90);
            
        }
        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {               
                await UserVM?.GetUserById();
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
        #region Show Info
        private void ShowPersoInfo_Clicked(object sender, EventArgs e)
        {
            IsShowPersoInfo = !IsShowPersoInfo;
            if (IsShowPersoInfo)
            {
                PersoInfoEntry.IsVisible = true;
                PersoInfoArrow.RelRotateTo(180, 100);
            }
            else
            {
                PersoInfoEntry.IsVisible = false;
                PersoInfoArrow.RelRotateTo(-180, 100);
            }
        }
        private void ShowBio_Clicked(object sender, EventArgs e)
        {
            IsShowBio = !IsShowBio;
            if (IsShowBio)
            {
                BioEntry.IsVisible = true;
                BioArrow.RelRotateTo(180, 100);
            }
            else
            {
                BioEntry.IsVisible = false;
                BioArrow.RelRotateTo(-180, 100);
            }
        }
        private void ShowLocationInfo_Clicked(object sender, EventArgs e)
        {
            IsShowLocationInfo = !IsShowLocationInfo;
            if (IsShowLocationInfo)
            {
                LocationInfoEntry.IsVisible = true;
                LocationInfoArrow.RelRotateTo(180, 100);
            }
            else
            {
                LocationInfoEntry.IsVisible = false;
                LocationInfoArrow.RelRotateTo(-180, 100);
            }
        }
        private void ShowAccountInfo_Clicked(object sender, EventArgs e)
        {
            IsShowAccountInfo = !IsShowAccountInfo;
            if (IsShowAccountInfo)
            {
                AccountInfoEntry.IsVisible = true;
                AccountInfoArrow.RelRotateTo(180, 100);
            }
            else
            {
                AccountInfoEntry.IsVisible = false;
                AccountInfoArrow.RelRotateTo(-180, 100);
            }
        }
        private void ShowLanguage_Clicked(object sender, EventArgs e)
        {
            IsShowLanguage = !IsShowLanguage;
            if (IsShowLanguage)
            {
                LanguageEntry.IsVisible = true;
                LanguageArrow.RelRotateTo(180, 100);
            }
            else
            {
                LanguageEntry.IsVisible = false;
                LanguageArrow.RelRotateTo(-180, 100);
            }
        }
        #endregion

        private async void ShowAvailibility_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AvailibilityGuideEditPage());
        }
    }
}