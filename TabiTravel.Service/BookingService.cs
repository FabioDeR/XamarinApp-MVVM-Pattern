using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.Service;
using TabiTravel.Service.Interface;
using TabiTravel.XamarinModel.Models.BookingModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(BookingService))]
namespace TabiTravel.Service
{
    public class BookingService : IBookingService
    {
        private HttpClient _httpClient;
        public BookingService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigService.Config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        #region Get By BookingId + UserId
        public async Task<BookingDetailVM> GetBookingDetail(int bookingId, Guid userId)
        {
            try
            {
                BookingDetailVM bookingDetailVM = new BookingDetailVM();
                string responseContent = await _httpClient.GetStringAsync($"api/Booking/getbookingdetailvm/{bookingId}/{userId}");
                bookingDetailVM = JsonConvert.DeserializeObject<BookingDetailVM>(responseContent);
                return bookingDetailVM;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Post(PreBookingVM)
        public async Task<HttpResponseMessage> PostPreBookingVM(PreBookingVM preBookingVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(preBookingVM);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseContent = await _httpClient.PostAsync("api/Booking/prebookingvm", stringContent);                  
                return responseContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Get All Booking List (UserId,Iso)
        public async Task<ObservableCollection<BookingListVM>> GetManageBookingList(Guid userId, string languageIso)
        {
            try
            {
                string responContent = await _httpClient.GetStringAsync($"api/Booking/managebookinglistvm/{userId}/{languageIso}");
                ObservableCollection<BookingListVM> bookingListVMs = JsonConvert.DeserializeObject<ObservableCollection<BookingListVM>>(responContent);
                return bookingListVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<ObservableCollection<BookingListVM>> GetHistoricBookingList(Guid userId, string languageIso)
        {
            try
            {
                string responContent = await _httpClient.GetStringAsync($"api/Booking/historicboolinglistvm/{userId}/{languageIso}");
                ObservableCollection<BookingListVM> bookingListVMs = JsonConvert.DeserializeObject<ObservableCollection<BookingListVM>>(responContent);
                return bookingListVMs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion
        #region Update
        public async Task<HttpResponseMessage> PutBookingVM(BookingVM bookingVM)
        {
            try
            {

                string jsonData = JsonConvert.SerializeObject(bookingVM);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseContent = await _httpClient.PutAsync("api/Booking/putbookingvm", stringContent);
                return responseContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        #endregion
        public async Task SoftDeletePreBooking(int bookingId, Guid userId)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/Booking/bookingdelete/{bookingId}/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<HttpResponseMessage> CheckErrors(PreBookingVM preBookingVM)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(preBookingVM);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseContent = await _httpClient.PostAsync("api/Booking/errorprebookingvm", stringContent);
                return responseContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
