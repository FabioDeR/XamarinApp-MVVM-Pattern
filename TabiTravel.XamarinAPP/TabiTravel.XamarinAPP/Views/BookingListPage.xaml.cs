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
    public partial class BookingListPage : ContentPage
    {
        BookingListPageVM BookingListPageVM;
        public BookingListPage()
        {
            InitializeComponent();
            BookingListPageVM = new BookingListPageVM(Navigation, this);
            BindingContext = BookingListPageVM;            
        }

        protected async override void OnAppearing()
        {
            Loader.IsVisible = true;
            try
            {
                await BookingListPageVM?.LoadManageBookingList();
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            PageContent.IsVisible = true;
            Loader.IsVisible = false;

        }
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selectedItem = e.SelectedItem as BookingListVM; 
                await Navigation.PushAsync(new BookingDetailPage(selectedItem.BookingId,false));                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}