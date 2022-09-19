using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoiEditPage : ContentPage
    {
        public FileResult ResultFile { get; set; }
        IInterestService InterestService;
        public bool IsShowDetail { get; set; } = true;
        public bool IsShowLocation { get; set; } = true;
        public bool IsShowSchedule { get; set; }
        public bool IsShowPricing { get; set; }
        public bool IsShowCategories { get; set; }
        public bool IsShowAccessibilities { get; set; }

        int InterestId;
        InterestListVM InterestVM;
        InterestDetailsVM InteresDetailtVM;
        public PoiEditPage()
        {
            InitializeComponent();
            InterestVM = new InterestListVM(Navigation, this);
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            BindingContext = InterestVM;
            GetLocationUser();
            UpdateButton.IsVisible = false;
        }

        public PoiEditPage(double longitude, double latitude)
        {
            InitializeComponent();
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            InterestVM = new InterestListVM(Navigation, this);
            InterestVM.Interest.Longitude = longitude;
            InterestVM.Interest.Latitude = latitude;
            InterestVM.FeedForms(latitude, longitude);
            BindingContext = InterestVM;
            UpdateButton.IsVisible = false;
        }

        public PoiEditPage(int Id)
        {
            InitializeComponent();
            InteresDetailtVM = new InterestDetailsVM(Navigation, Id, this);
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
            BindingContext = InteresDetailtVM;
            InteresDetailtVM?.LoadInterest(Id);
            InterestId = Id;
            SaveButton.IsVisible = false;

        }

        protected async void GetLocationUser()
        {
            var location = CrossGeolocator.Current;
            Plugin.Geolocator.Abstractions.Position position = await location.GetPositionAsync();
            InterestVM.Interest.Longitude = position.Longitude;
            InterestVM.Interest.Latitude = position.Latitude;
            InterestVM.FeedForms(position.Latitude, position.Longitude);
        }
        protected override void OnAppearing()
        {
            try
            {
                DetailEntry.IsVisible = true;
                DetailArrow.RelRotateTo(180, 100);
                LocationEntry.IsVisible = true;
                LocationArrow.RelRotateTo(180, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            base.OnAppearing();
        }
        #region Show Details
        private void ShowDetail_Clicked(object sender, EventArgs e)
        {
            IsShowDetail = !IsShowDetail;
            if (IsShowDetail)
            {
                DetailEntry.IsVisible = true;
                DetailArrow.RelRotateTo(180, 100);

            }
            else
            {
                DetailEntry.IsVisible = false;
                DetailArrow.RelRotateTo(-180, 100);
            }
        }
        private void ShowLocation_Clicked(object sender, EventArgs e)
        {
            IsShowLocation = !IsShowLocation;
            if (IsShowLocation)
            {
                LocationEntry.IsVisible = true;
                LocationArrow.RelRotateTo(180, 100);

            }
            else
            {
                LocationEntry.IsVisible = false;
                LocationArrow.RelRotateTo(-180, 100);
            }
        }

        private void ShowSchedule_Clicked(object sender, EventArgs e)
        {
            IsShowSchedule = !IsShowSchedule;
            if (IsShowSchedule)
            {
                ScheduleEntry.IsVisible = true;
                ScheduleArrow.RelRotateTo(180, 100);

            }
            else
            {
                ScheduleEntry.IsVisible = false;
                ScheduleArrow.RelRotateTo(-180, 100);
            }
        }

        private void ShowPricing_Clicked(object sender, EventArgs e)
        {
            IsShowPricing = !IsShowPricing;
            if (IsShowPricing)
            {
                PricingEntry.IsVisible = true;
                PricingArrow.RelRotateTo(180, 100);

            }
            else
            {
                PricingEntry.IsVisible = false;
                PricingArrow.RelRotateTo(-180, 100);
            }
        }


        private void ShowCategories_Clicked(object sender, EventArgs e)
        {
            IsShowCategories = !IsShowCategories;
            if (IsShowCategories)
            {
                CategoriesEntry.IsVisible = true;
                CategoriesArrow.RelRotateTo(180, 100);

            }
            else
            {
                CategoriesEntry.IsVisible = false;
                CategoriesArrow.RelRotateTo(-180, 100);
            }
        }

        private void ShowAccessibilities_Clicked(object sender, EventArgs e)
        {
            IsShowAccessibilities = !IsShowAccessibilities;
            if (IsShowAccessibilities)
            {
                AccessibilitiesEntry.IsVisible = true;
                AccessibilitiesArrow.RelRotateTo(180, 100);

            }
            else
            {
                AccessibilitiesEntry.IsVisible = false;
                AccessibilitiesArrow.RelRotateTo(-180, 100);
            }
        }
        #endregion

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    EntryPricePerChild.IsVisible = false;
                    EntryPricePerAdult.IsVisible = false;
                }
                else
                {
                    EntryPricePerChild.IsVisible = true;
                    EntryPricePerAdult.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entryNumber = (Entry)sender;

                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPA")
                {
                    if (InterestId == 0)
                    {
                        InterestVM.Interest.PricePerAdult = null;
                    }
                    else
                    {
                        InteresDetailtVM.Interest.PricePerAdult = null;
                    }
                }
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPC")
                {
                    if (InterestId == 0)
                    {
                        InterestVM.Interest.PricePerChild = null;
                    }
                    else
                    {
                        InteresDetailtVM.Interest.PricePerChild = null;
                    }
                }
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPV")
                {
                    if (InterestId == 0)
                    {
                        InterestVM.Interest.PricePerChild = null;
                    }
                    else
                    {
                        InteresDetailtVM.Interest.PricePerVehicule = null;
                    }
                }
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPE")
                {
                    if (InterestId == 0)
                    {
                        InterestVM.Interest.EmplacementAvailable = null;
                    }
                    else
                    {
                        InteresDetailtVM.Interest.EmplacementAvailable = null;
                    }
                }
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "ET")
                {
                    if (InterestId == 0)
                    {
                        InterestVM.Interest.EstimatedTime = null;
                    }
                    else
                    {
                        InteresDetailtVM.Interest.EstimatedTime = null;
                    }
                }                
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "CY")
                {
                    if (InterestId == 0)
                    {
                        InterestVM.Interest.City = null;
                    }
                    else
                    {
                        InteresDetailtVM.Interest.City = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

       

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if(Int32.Parse(checkBox.ClassId) == 8)
            {
                FrameEmplacement.IsVisible = !FrameEmplacement.IsVisible;
            }
        }
    }
}