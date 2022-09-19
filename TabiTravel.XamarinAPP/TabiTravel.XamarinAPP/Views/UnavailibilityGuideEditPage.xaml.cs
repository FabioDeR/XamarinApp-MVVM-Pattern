using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnavailibilityGuideEditPage : ContentPage
    {
        UnavailabilityGuideVM UnavailabilityGuideVM;

        public UnavailibilityGuideEditPage()
        {

            Guid userId = App.Database.GetUserId();
            InitializeComponent();
            UnavailabilityGuideVM = new UnavailabilityGuideVM(Navigation, this);
            UnavailabilityGuideVM.UnavailabilityGuideEditVM.UserId = userId;
            BindingContext = UnavailabilityGuideVM;
            DateStartPicker.MinimumDate = DateTime.Today;
            DateEndPicker.MinimumDate = DateTime.Today;
            SaveButton.IsVisible = true;
            UpdateButton.IsVisible = false;
            DeleteIcon.IsEnabled = false;
        }

        public UnavailibilityGuideEditPage(int unavailabilityGuideId)
        {
            InitializeComponent();
            UnavailabilityGuideVM = new UnavailabilityGuideVM(Navigation, this);           
            BindingContext = UnavailabilityGuideVM;

            UnavailabilityGuideVM.GetById(unavailabilityGuideId);

            SaveButton.IsVisible = false;
            UpdateButton.IsVisible = true;
            DeleteIcon.IsEnabled = true;
        }
    }
}