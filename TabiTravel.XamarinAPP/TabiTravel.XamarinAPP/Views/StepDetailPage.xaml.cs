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
    public partial class StepDetailPage : ContentPage
    {
        StepDetailVM StepDetailVM;
        int StepId = new int();
        int TourId;
        public StepDetailPage(int stepId, int? tourId = null)
        {
            InitializeComponent();
            StepDetailVM = new StepDetailVM(Navigation, this);
            BindingContext = StepDetailVM;
            StepId = stepId;
            if (tourId != null)
            {
                TourId = (int)tourId;
            }
        }
        protected async override void OnAppearing()
        {
            try
            {
                await StepDetailVM?.GetMyStepbyId(StepId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            base.OnAppearing();
        }
      
    }
}