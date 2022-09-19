using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TabiTravel.XamarinModel.Models.BookingModel
{
    public class BookingListVM : BaseModelVM
    {
        private int bookingId;
        private int tourId;
        private string tourImg;
        private string tourName;
        private string tourCountry;
        private string tourCity;
        private string guideName;
        private bool isAccepted;
        private bool isRefused;
        private bool isWaiting;
        private DateTime dateStart;
        private DateTime dateEnd;

        public int BookingId { get => bookingId; set { bookingId = value; OnPropertyChanged(nameof(BookingId));} }
        public int TourId { get => tourId; set { tourId = value; OnPropertyChanged(nameof(TourId));} }
        public string TourImg { get => tourImg; set { tourImg = value; OnPropertyChanged(nameof(TourImg));} }
        public string TourName { get => tourName; set { tourName = value; OnPropertyChanged(nameof(TourName));} }
        public string TourCountry { get => tourCountry; set { tourCountry = value; OnPropertyChanged(nameof(TourCountry));} }
        public string TourCity { get => tourCity; set { tourCity = value; OnPropertyChanged(nameof(TourCity));} }
        public string GuideName { get => guideName; set { guideName = value; OnPropertyChanged(nameof(GuideName));} }
        public bool IsAccepted { get => isAccepted; set { isAccepted = value; OnPropertyChanged(nameof(IsAccepted));} }
        public bool IsRefused { get => isRefused; set { isRefused = value; OnPropertyChanged(nameof(IsRefused));} }
        public bool IsWaiting { get => isWaiting; set { isWaiting = value; OnPropertyChanged(nameof(IsWaiting));} }
        public DateTime DateStart { get => dateStart; set { dateStart = value; OnPropertyChanged(nameof(DateStart));} }
        public DateTime DateEnd { get => dateEnd; set { dateEnd = value; OnPropertyChanged(nameof(DateEnd));} }
    }
}
