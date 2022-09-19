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
    public partial class TourEditPage : ContentPage
    {
        public bool IsShowPricing { get; set; }
        public int TourId { get; set; }
        TourEditPageVM TourEditPageVM;
        TourEditDetailVM TourEditDetailVM;


        public TourEditPage()
        {
            InitializeComponent();
            TourEditPageVM = new TourEditPageVM(Navigation, this);            
            BindingContext = TourEditPageVM;
            UpdateButton.IsVisible = false;
            TourEditPageVM?.LoadCategoryTour();
            TourEditPageVM?.LoadCountry();
        }

        public TourEditPage(int tourId)
        {
            InitializeComponent();
            TourEditDetailVM = new TourEditDetailVM(Navigation, this);            
            BindingContext = TourEditDetailVM;
            TourId = tourId;
            SaveButton.IsVisible = false;
            TourEditDetailVM?.LoadCategoryTour();
            TourEditDetailVM?.LoadCountry();
            TourEditDetailVM?.GetTourById(TourId);
        }

        #region HandelSwitch
        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            try
            {
                if (e.Value)
                {
                    EntryPricePerChild.IsVisible = false;
                    EntryPricePerAdult.IsVisible = false;
                    EntryPricePerVehicule.IsVisible = false;                   
                }
                else
                {
                    EntryPricePerChild.IsVisible = true;
                    EntryPricePerAdult.IsVisible = true;
                    EntryPricePerVehicule.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
       
        #endregion

        #region Entry Text Control
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entryNumber = (Entry)sender;

                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPA")
                {
                    if (TourId == 0)
                    {
                        TourEditPageVM.Tour.PricePerAdult = null;
                    }
                    else
                    {
                        TourEditDetailVM.Tour.PricePerAdult = null;
                    }
                }
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPC")
                {
                    if (TourId == 0)
                    {
                        TourEditPageVM.Tour.PricePerChild = null;
                    }
                    else
                    {
                        TourEditDetailVM.Tour.PricePerChild = null;
                    }
                }
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PPV")
                {
                    if (TourId == 0)
                    {
                        TourEditPageVM.Tour.PricePerVehicule = null;
                    }
                    else
                    {
                        TourEditDetailVM.Tour.PricePerVehicule = null;
                    }
                }                 
                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "CY")
                {
                    if (TourId == 0)
                    {
                        TourEditPageVM.Tour.City = null;
                    }
                    else
                    {
                        TourEditDetailVM.Tour.City = null;
                    }
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