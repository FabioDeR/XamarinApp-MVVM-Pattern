using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinAPP.Views;
using TabiTravel.XamarinModel.Models.BookingModel;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.Booking
{
    public class BookingDetailPageVM : BaseBookingVM
    {
        private BookingDetailVM bookingDetailVM;
        public BookingDetailVM BookingDetailVM { get => bookingDetailVM; set { bookingDetailVM = value; OnPropertyChanged(nameof(BookingDetailVM)); } }
        private int BookingId;
        BookingVM BookingVM { get; set; }

        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); } }



        public BookingDetailPageVM(INavigation navigation, Page page, int bookingIg) : base(navigation, page)
        {
            Update = new Command(UpdateAccepted);
            UpdateTwo = new Command(UpdateRefused);
            BookingDetailVM = new BookingDetailVM();
            BookingId = bookingIg;
            GoTo = new Command(GoToTourDetail);
            BookingVM = new BookingVM();
        }

        private async void GoToTourDetail(object obj)
        {
            await Navigation.PushAsync(new TourDetailPage(BookingDetailVM.TourId,BookingDetailVM.IsGuide));
        }

        public async Task GetBookingDetail()
        {
            try
            {
                BookingDetailVM = await BookingService.GetBookingDetail(BookingId, UserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        private async void UpdateRefused(object obj)
        {
            try
            {
                BookingVM.RefusedDate = DateTime.Now;
                BookingVM.BookingId = BookingId;
                BookingVM.UserId = BookingDetailVM.GuideId;
                var res = await BookingService.PutBookingVM(BookingVM);
                if (res.IsSuccessStatusCode)
                {
                    /*Resfreh*/
                    await Page.DisplayAlert("Sucess", "Item has Updated", "Ok");
                    await Navigation.PushAsync(new BookingListPage());
                    Navigation.RemovePage(Page);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private async void UpdateAccepted(object obj)
        {
            try
            {
                BookingVM.AcceptedDate = DateTime.Now;
                BookingVM.BookingId = BookingId;
                BookingVM.UserId = BookingDetailVM.GuideId;
                var res = await BookingService.PutBookingVM(BookingVM);
                if (res.IsSuccessStatusCode)
                {
                    /*Resfreh*/
                    await Page.DisplayAlert("Sucess", "Item has Updated", "Ok");
                    await Navigation.PushAsync(new BookingListPage());
                    Navigation.RemovePage(Page);
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
