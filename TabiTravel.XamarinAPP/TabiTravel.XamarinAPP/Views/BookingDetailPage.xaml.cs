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
    public partial class BookingDetailPage : ContentPage
    {
        BookingDetailPageVM BookingDetailPageVM;
        private bool IsTourist { get; set; } = false;
        public bool IsAccepted { get; set; } = false;
        public bool IsRefused { get; set; } = false;
        public bool IsWaiting { get; set; } = false;
        public BookingDetailPage(int bookingId, bool isTourist)
        {
            InitializeComponent();
            BookingDetailPageVM = new BookingDetailPageVM(Navigation, this, bookingId);
            BindingContext = BookingDetailPageVM;
            IsTourist = isTourist;
        }

        protected async override void OnAppearing()
        {
            try
            {
                await BookingDetailPageVM.GetBookingDetail();
                if (BookingDetailPageVM.BookingDetailVM.RefusedDate != new DateTime())
                {
                    IsRefused = true;
                    StackLayoutColor.BackgroundColor = Color.FromHex("E24C4B");
                    ImageStack.Source = "whiterefused";
                    LabelStack.Text = "Refused";
                    StackButton.IsVisible = false;
                }
                if (BookingDetailPageVM.BookingDetailVM.AcceptedDate != new DateTime())
                {
                    IsAccepted = true;
                    StackLayoutColor.BackgroundColor = Color.FromHex("5CB85C");
                    ImageStack.Source = "whiteaccepted";
                    LabelStack.Text = "Accepted";
                    StackButton.IsVisible = false;
                }
                if (BookingDetailPageVM.BookingDetailVM.RefusedDate.Date == new DateTime() && BookingDetailPageVM.BookingDetailVM.AcceptedDate.Date == new DateTime())
                {
                    IsWaiting = true;
                    StackLayoutColor.BackgroundColor = Color.FromHex("FCA120");
                    ImageStack.Source = "whitewaiting";
                    LabelStack.Text = "Pending";
                    if (IsTourist)
                    {
                        StackButton.IsVisible = false;
                    }
                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           

        }
    }
}