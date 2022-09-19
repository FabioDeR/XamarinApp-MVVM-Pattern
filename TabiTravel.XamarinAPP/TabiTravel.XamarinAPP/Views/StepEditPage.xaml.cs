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
    public partial class StepEditPage : ContentPage
    {
        private StepVM StepVM;
        private StepEditDetailVM StepEditDetailVM;
        private int TourId = new int();
        private int Days = new int();
        private int StepId = new int();

        public StepEditPage(int tourId, int dayId)
        {
            InitializeComponent();
            StepVM = new StepVM(Navigation, this);
            BindingContext = StepVM;
            TourId = tourId;
            Days = dayId;
            StepId = 0;
            UpdateButton.IsVisible = false;          
        }
        public StepEditPage(int stepId)
        {
            InitializeComponent();
            StepEditDetailVM = new StepEditDetailVM(Navigation, this, stepId);
            BindingContext = StepEditDetailVM;
            StepId = stepId;
            SaveButton.IsVisible = false;            
        }
        protected override void OnAppearing()
        {
            try
            {
                if (StepId == 0)
                {
                    StepVM.StepEdit.TourId = (int)TourId;
                    StepVM.StepEdit.Day = (int)Days;
                    StepVM?.LoadInterestList();
                    StepVM?.LoadUnit();
                    ToolBarStep.IsEnabled = false;                    
                }
                else
                {
                    StepEditDetailVM?.GetStepId();                                      
                    ToolBarStep.IsEnabled = true;
                }
                base.OnAppearing();
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

                if (e.NewTextValue == string.Empty && entryNumber.ClassId == "PS")
                {
                    if (StepId == 0)
                    {
                        StepVM.StepEdit.PreviousStep = 0;
                        
                    }
                    else
                    {
                        StepEditDetailVM.StepEdit.PreviousStep = 0;
                    }
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
            //  ::::: /!\ Need TourId to return on TourDetailPage after SoftDelete /!\ :::::
            await Navigation.PushPopupAsync(new MoreMenuPage(this,StepId, NavigationEnum.StepEdit, EnumTranslate.Step, StepEditDetailVM?.StepEdit.TourId), true);
        }
    }
}