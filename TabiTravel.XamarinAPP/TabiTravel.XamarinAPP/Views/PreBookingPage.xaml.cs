using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels.Booking;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreBookingPage : ContentPage
    {
        PreBookingPageVM PreBookingPageVM;

        public PreBookingPage(int tourid)
        {
            PreBookingPageVM = new PreBookingPageVM(Navigation, this, tourid);
            BindingContext = PreBookingPageVM;
            InitializeComponent();
            DateStartPicker.MinimumDate = DateTime.Today;
            
        }

        protected override void OnAppearing()
        {
            try
            {
                PreBookingPageVM?.LoadTour();
                AdultNumberLabel.Text = PreBookingPageVM.AdultNumber.ToString();
                ChildNumberLabel.Text = PreBookingPageVM.ChildNumber.ToString();
                EmplacementNumberLabel.Text = PreBookingPageVM.EmplacementNumber.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }

        


        #region Adult Custom Stepper
        private void AdultMinusButton_Clicked(object sender, EventArgs e)
        {
            if (PreBookingPageVM.AdultNumber > 0)
            {
                BtnMinusAdult.Source = "btnminus";
                BtnMinusAdult.IsEnabled = true;
                PreBookingPageVM.AdultNumber--;
                AdultNumberLabel.Text = PreBookingPageVM.AdultNumber.ToString();
            }
            if (PreBookingPageVM.AdultNumber == 0)
            {
                BtnMinusAdult.Source = "btnminusdisabled";
                BtnMinusAdult.IsEnabled = false;
            }
            if (PreBookingPageVM.AdultNumber < 10)
            {
                BtnPlusAdult.Source = "btnplus";
                BtnPlusAdult.IsEnabled = true;
            }
        }
        private void AdultPlusButton_Clicked(object sender, EventArgs e)
        {
            if (PreBookingPageVM.AdultNumber <= 9)
            {
                BtnPlusAdult.Source = "btnplus";
                BtnPlusAdult.IsEnabled = true;
                PreBookingPageVM.AdultNumber++;
                AdultNumberLabel.Text = PreBookingPageVM.AdultNumber.ToString();
            }
            if (PreBookingPageVM.AdultNumber > 9)
            {
                BtnPlusAdult.Source = "btnplusdisabled";
                BtnPlusAdult.IsEnabled = false;
            }
            if (PreBookingPageVM.AdultNumber > 0)
            {
                BtnMinusAdult.Source = "btnminus";
                BtnMinusAdult.IsEnabled = true;
            }
        }
        #endregion

        #region Child Custom Stepper
        private void ChildMinusButton_Clicked(object sender, EventArgs e)
        {
            if (PreBookingPageVM.ChildNumber > 0)
            {
                BtnMinusChild.Source = "btnminus";
                BtnMinusChild.IsEnabled = true;
                PreBookingPageVM.ChildNumber--;
                ChildNumberLabel.Text = PreBookingPageVM.ChildNumber.ToString();
            }
            if (PreBookingPageVM.ChildNumber == 0)
            {
                BtnMinusChild.Source = "btnminusdisabled";
                BtnMinusChild.IsEnabled = false;
            }
            if (PreBookingPageVM.ChildNumber < 10)
            {
                BtnPlusChild.Source = "btnplus";
                BtnPlusChild.IsEnabled = true;
            }
        }
        private void ChildPlusButton_Clicked(object sender, EventArgs e)
        {
            if (PreBookingPageVM.ChildNumber <= 9)
            {
                BtnPlusChild.Source = "btnplus";
                BtnPlusChild.IsEnabled = true;
                PreBookingPageVM.ChildNumber++;
                ChildNumberLabel.Text = PreBookingPageVM.ChildNumber.ToString();
            }
            if (PreBookingPageVM.ChildNumber > 9)
            {
                BtnPlusChild.Source = "btnplusdisabled";
                BtnPlusChild.IsEnabled = false;
            }
            if (PreBookingPageVM.ChildNumber > 0)
            {
                BtnMinusChild.Source = "btnminus";
                BtnMinusChild.IsEnabled = true;
            }
        }
        #endregion

        #region Emplacement Custom Stepper
        private void EmplacementMinusButton_Clicked(object sender, EventArgs e)
        {
            if (PreBookingPageVM.EmplacementNumber > 0)
            {
                BtnMinusEmplacement.Source = "btnminus";
                BtnMinusEmplacement.IsEnabled = true;
                PreBookingPageVM.EmplacementNumber--;
                EmplacementNumberLabel.Text = PreBookingPageVM.EmplacementNumber.ToString();
            }
            if (PreBookingPageVM.EmplacementNumber == 0)
            {
                BtnMinusEmplacement.Source = "btnminusdisabled";
                BtnMinusEmplacement.IsEnabled = false;
            }
            if (PreBookingPageVM.EmplacementNumber < 10)
            {
                BtnPlusEmplacement.Source = "btnplus";
                BtnPlusEmplacement.IsEnabled = true;
            }
        }
        private void EmplacementPlusButton_Clicked(object sender, EventArgs e)
        {
            if (PreBookingPageVM.EmplacementNumber <= 9)
            {
                BtnPlusEmplacement.Source = "btnplus";
                BtnPlusEmplacement.IsEnabled = true;
                PreBookingPageVM.EmplacementNumber++;
                EmplacementNumberLabel.Text = PreBookingPageVM.EmplacementNumber.ToString();
            }
            if (PreBookingPageVM.EmplacementNumber > 9)
            {
                BtnPlusEmplacement.Source = "btnplusdisabled";
                BtnPlusEmplacement.IsEnabled = false;
            }
            if (PreBookingPageVM.EmplacementNumber > 0)
            {
                BtnMinusEmplacement.Source = "btnminus";
                BtnMinusEmplacement.IsEnabled = true;
            }
        }
        #endregion
    }
}