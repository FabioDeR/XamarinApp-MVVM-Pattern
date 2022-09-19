using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models.BookingModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.Booking
{
    public class PreBookingDetailPageVM : BaseBookingVM
    {
        public int BookingId { get; }
        private BookingDetailVM bookingDetailVM;
        public BookingDetailVM BookingDetailVM { get => bookingDetailVM; set { bookingDetailVM = value; OnPropertyChanged(nameof(BookingDetailVM)); } }
        private PreBookingVM preBookingVM;
        public PreBookingVM PreBookingVM { get => preBookingVM; set { preBookingVM = value; OnPropertyChanged(nameof(PreBookingVM)); } }
        public Page PreBookingPage { get; }
        private bool isAccepted;
        public bool IsAccepted { get => isAccepted; set { isAccepted = value; OnPropertyChanged(nameof(BookingDetailVM)); } } 
        public PreBookingDetailPageVM(INavigation navigation, Page page, BookingDetailVM bookingDetailVM, Page preBookingPage,PreBookingVM preBookingVM) : base(navigation, page)
        {
            GoTo = new Command(PopModal);
            BookingDetailVM = bookingDetailVM;
            PreBookingPage = preBookingPage;
            PreBookingVM = preBookingVM;
            IsAccepted = false;
        }     

        private async void PopModal(object obj)
        {
            try
            {
                
                var res = await BookingService.PostPreBookingVM(PreBookingVM);
                if (res.IsSuccessStatusCode)
                {
                    IsAccepted = true;
                    await Navigation.PopPopupAsync();
                    await ((MainPage)App.Current.MainPage).Detail.Navigation.PushAsync(new HistoricBookingListPage());
                    ((MainPage)App.Current.MainPage).Detail.Navigation.RemovePage(PreBookingPage);
                }         
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
