using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models.BookingModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.Booking
{
    public class BookingListPageVM : BaseBookingVM
    {
        private ObservableCollection<BookingListVM> bookingListVM;
        public ObservableCollection<BookingListVM> BookingListVM { get => bookingListVM; set { bookingListVM = value;OnPropertyChanged(nameof(BookingListVM)); } }

        private BookingListVM bookingSelected;
        public BookingListVM BookingSelected { get => bookingSelected; set { bookingSelected = value;OnPropertyChanged(nameof(BookingSelected)); } }

        public BookingListPageVM(INavigation navigation, Page page) : base(navigation, page)
        {
            
        }


        public async Task LoadManageBookingList()
        {
            try
            {
                BookingListVM = await BookingService.GetManageBookingList(UserId, MotherLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task LoadHistoricBooking()
        {
            try
            {
                /**Change when System Auth**/
                BookingListVM = await BookingService.GetHistoricBookingList(UserId, MotherLanguage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
