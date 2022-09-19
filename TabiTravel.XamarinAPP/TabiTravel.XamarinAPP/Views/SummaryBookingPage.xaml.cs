using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels.Booking;
using TabiTravel.XamarinModel.Models.BookingModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryBookingPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        PreBookingDetailPageVM PreBookingPageVM;
        public SummaryBookingPage(BookingDetailVM preBookingDetail, Page pagePrebooking,PreBookingVM preBookingVM )
        {
            InitializeComponent();
            PreBookingPageVM = new PreBookingDetailPageVM(Navigation, this, preBookingDetail,pagePrebooking,preBookingVM);
            BindingContext = PreBookingPageVM;            
        }      
    }
}