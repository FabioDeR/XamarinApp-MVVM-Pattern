using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using TabiTravel.XamarinModel.Models.InterestModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoiDetailPage : ContentPage
    {
        public int InterestId { get; set; }
        public bool IsGuid { get; }

        public InterestDetailsVM interestDetailsVM;

        public PoiDetailPage(int Id, bool isGuid)
        {
            InitializeComponent();
            InterestId = Id;
            interestDetailsVM = new InterestDetailsVM(Navigation, Id, this);
            BindingContext = interestDetailsVM;
            IsGuid = isGuid;
        }

        protected async override void OnAppearing()
        {
            if (!IsGuid)
            {
                ToolBarPOI.IsEnabled = false;
                ToolBarPOI.IconImageSource = "";
            }
            Loader.IsVisible = true;
            await interestDetailsVM?.LoadInterest(InterestId);            
            Interest interest = interestDetailsVM.Interest;
            #region Image
            if (interest.ImageUrl == null)
            {
                InterestImage.Source = "nopicture";
            }
            #endregion

            #region Content
            if (interestDetailsVM.Content == null || interestDetailsVM.Content == string.Empty)
            {
                ContentArea.IsVisible = false;
            }
            #endregion

            #region Categories
            if (interestDetailsVM.CategoryListDetail.Count == 0)
            {
                CategoriesArea.IsVisible = false;
            }
            #endregion

            #region Info Block
            if (!interest.IsFree && interest.EstimatedTime == null)
            {
                InfoBlock.IsVisible = false;
            }
            #endregion

            #region Schedule

            if (!interest.IsMonday && !interest.IsTuesday && !interest.IsWednesday && !interest.IsThursday && !interest.IsFriday && !interest.IsSaturday && !interest.IsSunday)
            {
                OpeningArea.IsVisible = false;
            }

            if (interest.Schedule == null || interest.Schedule == string.Empty)
            {
                ScheduleArea.IsVisible = false;
            }
            #endregion

            #region Pricing
            if (interest.PricePerAdult == null)
            {
                PricingAdultLabel.IsVisible = false;
            }
            if (interest.PricePerChild == null)
            {
                PricingChildLabel.IsVisible = false;
            }
            if (interest.PricePerVehicule == null)
            {
                PricingVehiculeLabel.IsVisible = false;
            }

            if (interest.PricePerAdult == null && interest.PricePerChild == null && interest.PricePerVehicule == null)
            {
                PricingArea.IsVisible = false;
            }

            if (interest.EstimatedTime == null)
            {
                ShowTime.IsVisible = false;
            }

            #endregion

            #region Address
            if (interest.Address == null || interest.Address == string.Empty)
            {
                AddressLabel.IsVisible = false;
            }
            if (interest.ZipCode == null || interest.ZipCode == string.Empty)
            {
                ZipLabel.IsVisible = false;
            }
            if (interest.City == null || interest.City == string.Empty)
            {
                CityLabel.IsVisible = false;
            }
            if (interestDetailsVM.CountryName == null || interestDetailsVM.CountryName == string.Empty)
            {
                CountryLabel.IsVisible = false;
            }
            if (interest.Address == null && interest.ZipCode == null && interest.City == null && interestDetailsVM.CountryName == null)
            {
                AddressArea.IsVisible = false;
            }
            #endregion

            #region Accessibility
            if (!interest.IsAnimalFriendly && !interest.IsChildFriendly && !interest.IsFree && !interest.IsPRMFriendly && !interest.IsStrollerFriendly)
            {
                AccessibilityArea.IsVisible = false;
            }
            #endregion

            #region Informations
            if (interest.Phone == string.Empty || interest.Phone == null)
            {
                PhoneLabel.IsVisible = false;
            }
            if (interest.Email == string.Empty || interest.Email == null)
            {
                EmailLabel.IsVisible = false;
            }
            if (interest.Url == string.Empty || interest.Url == null)
            {
                WebsiteLabel.IsVisible = false;
            }
            if ((interest.Phone == string.Empty || interest.Phone == null) && (interest.Email == string.Empty || interest.Email == null) && (interest.Url == string.Empty || interest.Url == null))
            {
                InformationsArea.IsVisible = false;
            }
            #endregion
            PageContent.IsVisible = true;
            Loader.IsVisible = false;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new MoreMenuPage(this,InterestId, NavigationEnum.InterestEdit, EnumTranslate.Interest), true);
        }
    }
}