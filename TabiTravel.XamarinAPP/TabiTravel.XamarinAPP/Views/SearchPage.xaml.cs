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
    public partial class SearchPage : ContentPage
    {
        #region Bool
        public bool IsShowAccessibilities { get; set; }
        public bool IsShowPricing { get; set; }    
        public bool IsShowPlusSearch { get; set; }
        public bool IsShowSchedule { get; set; }

        #endregion
        #region VM
        SearchPageVM SearchPageVM;
        #endregion
        public SearchPage(EnumSearch enumSearch)
        {
            InitializeComponent();   
            SearchPageVM = new SearchPageVM(Navigation,this,enumSearch);
            BindingContext = SearchPageVM;
            SearchPageVM?.LoadCategoryAndLanguage();
        }
        #region Arrow Method
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
        private void ShowAccessibilities_Clicked(object sender, EventArgs e)
        {
            IsShowAccessibilities = !IsShowAccessibilities;
            if (IsShowAccessibilities)
            {
                AccessibilitiesEntry.IsVisible = true;
                AccessibilitiesArrow.RelRotateTo(180, 100);

            } else
            {
                AccessibilitiesEntry.IsVisible = false;
                AccessibilitiesArrow.RelRotateTo(-180, 100);
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
        private void ShowPlusSearch_Clicked(object sender, EventArgs e)
        {
            IsShowPlusSearch =! IsShowPlusSearch;
            if (IsShowPlusSearch)
            {
                PlusSearch.IsVisible = true;
                PlusSearchArrow.RelRotateTo(180, 100);
            }
            else
            {
                PlusSearch.IsVisible = false;
                PlusSearchArrow.RelRotateTo(-180, 100);
            }
        }
        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    EntryPricePerChild.IsEnabled = false;
                    EntryPriceAdult.IsEnabled = false;                    
                }
                else
                {
                    EntryPricePerChild.IsEnabled = true;
                    EntryPriceAdult.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        void Handle_ToggledParking(object sender, ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    EntryPricePerVehicule.IsEnabled = false;                    
                }
                else
                {
                    EntryPricePerVehicule.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
    }
}