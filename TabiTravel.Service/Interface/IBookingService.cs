using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinModel.Models.BookingModel;

namespace TabiTravel.Service.Interface
{
    public interface IBookingService
    {
        Task<BookingDetailVM> GetBookingDetail(int bookingId, Guid userId);
        Task<ObservableCollection<BookingListVM>> GetManageBookingList(Guid userId, string languageIso);
        Task<HttpResponseMessage> PostPreBookingVM(PreBookingVM preBookingVM);
        Task<ObservableCollection<BookingListVM>> GetHistoricBookingList(Guid userId, string motherLanguage);
        Task<HttpResponseMessage> PutBookingVM(BookingVM bookingVM);
        Task SoftDeletePreBooking(int bookingId, Guid guid);
        Task<HttpResponseMessage> CheckErrors(PreBookingVM preBookingVM);
    }
}
