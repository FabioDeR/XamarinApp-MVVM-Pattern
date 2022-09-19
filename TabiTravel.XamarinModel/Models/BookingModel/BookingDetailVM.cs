using System;
using System.Collections.Generic;
using System.Text;

namespace TabiTravel.XamarinModel.Models.BookingModel
{
    public class BookingDetailVM : BaseModelVM
    {
        private string guideName;
        private string tourImg;
        private DateTime dateStart;
        private DateTime dateEnd;
        private string guideNickname;
        private string guidePhone;
        private string guideEmail;
        private string guideAvatar;
        private Guid guideId;
        private string touristName;
        private string touristPhone;
        private string touristEmail;
        private string touristAvatar;
        private Guid touristId;
        private DateTime acceptedDate;
        private DateTime refusedDate;
        private bool isGuide;
        private int nbChild;
        private int nbAdult;
        private int nbVehicule;
        private float? totalPriceChild;
        private float? totalPriceAdult;
        private float? totalPriceVehicule;
        private float? totalPrice;
        private int tourId;
        private string tourName;

        public string TourImg { get => tourImg; set { tourImg = value; OnPropertyChanged(nameof(TourImg)); } }
        public DateTime DateStart { get => dateStart; set { dateStart = value; OnPropertyChanged(nameof(DateStart)); } }
        public DateTime DateEnd { get => dateEnd; set { dateEnd = value; OnPropertyChanged(nameof(DateEnd)); } }
        public string GuideName { get => guideName; set { guideName = value; OnPropertyChanged(nameof(GuideName)); } }
        public string GuideNickname { get => guideNickname; set { guideNickname = value; OnPropertyChanged(nameof(GuideNickname)); } }
        public string GuidePhone { get => guidePhone; set { guidePhone = value; OnPropertyChanged(nameof(GuidePhone)); } }
        public string GuideEmail { get => guideEmail; set { guideEmail = value; OnPropertyChanged(nameof(GuideEmail)); } }
        public string GuideAvatar { get => guideAvatar; set { guideAvatar = value; OnPropertyChanged(nameof(GuideAvatar)); } }
        public Guid GuideId { get => guideId; set { guideId = value; OnPropertyChanged(nameof(GuideId)); } }
        public string TouristName { get => touristName; set { touristName = value; OnPropertyChanged(nameof(TouristName)); } }
        public string TouristPhone { get => touristPhone; set { touristPhone = value; OnPropertyChanged(nameof(TouristPhone)); } }
        public string TouristEmail { get => touristEmail; set { touristEmail = value; OnPropertyChanged(nameof(TouristEmail)); } }
        public string TouristAvatar { get => touristAvatar; set { touristAvatar = value; OnPropertyChanged(nameof(TouristAvatar)); } }
        public Guid TouristId { get => touristId; set { touristId = value; OnPropertyChanged(nameof(TouristId)); } }
        public DateTime AcceptedDate { get => acceptedDate; set { acceptedDate = value; OnPropertyChanged(nameof(AcceptedDate)); } }
        public DateTime RefusedDate { get => refusedDate; set { refusedDate = value; OnPropertyChanged(nameof(RefusedDate)); } }
        public bool IsGuide { get => isGuide; set { isGuide = value; OnPropertyChanged(nameof(IsGuide)); } }
        public int NbChild { get => nbChild; set { nbChild = value;OnPropertyChanged(nameof(NbChild)); } }
        public int NbAdult { get => nbAdult; set { nbAdult = value;OnPropertyChanged(nameof(NbAdult)); } }
        public int NbVehicule { get => nbVehicule; set { nbVehicule = value;OnPropertyChanged(nameof(NbVehicule)); } }
        public float? TotalPriceChild { get => totalPriceChild; set { totalPriceChild = value;OnPropertyChanged(nameof(TotalPriceChild)); } }
        public float? TotalPriceAdult { get => totalPriceAdult; set { totalPriceAdult = value;OnPropertyChanged(nameof(TotalPriceAdult)); } }
        public float? TotalPriceVehicule { get => totalPriceVehicule; set { totalPriceVehicule = value;OnPropertyChanged(nameof(TotalPriceVehicule)); } }
        public float? TotalPrice { get => totalPrice; set { totalPrice = value;OnPropertyChanged(nameof(TotalPrice)); } }  
        public int TourId { get => tourId; set { tourId = value;OnPropertyChanged(nameof(TourId)); } }
        public string TourName { get => tourName; set { tourName = value; OnPropertyChanged(nameof(TourName)); } }
    }
}
