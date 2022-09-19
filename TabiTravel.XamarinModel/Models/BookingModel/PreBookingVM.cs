using System;
using System.Collections.Generic;
using System.Text;

namespace TabiTravel.XamarinModel.Models.BookingModel
{
    public class PreBookingVM : BaseModelVM
    {
        private int tourId;
        private Guid guideId;
        private Guid touristId;
        private DateTime dateStart;
        private int nbrAdult;
        private int nbrChild;
        private int nbrVehicule;
        private string errorMessage;
        private bool isGuideUnavailability;
        private bool isEmplacementAvailable;
        private bool isInterestAvailable;

        public int TourId { get => tourId; set { tourId = value; OnPropertyChanged(nameof(TourId)); } }
        public Guid GuideId { get => guideId; set { guideId = value; OnPropertyChanged(nameof(GuideId)); } }
        public Guid TouristId { get => touristId; set { touristId = value; OnPropertyChanged(nameof(TouristId)); } }
        public DateTime DateStart { get => dateStart; set { dateStart = value; OnPropertyChanged(nameof(DateStart)); } }
        public int NbrAdult { get => nbrAdult; set { nbrAdult = value; OnPropertyChanged(nameof(NbrAdult)); } }
        public int NbrChild { get => nbrChild; set { nbrChild = value; OnPropertyChanged(nameof(NbrChild)); } }
        public int NbrVehicule { get => nbrVehicule; set { nbrVehicule = value; OnPropertyChanged(nameof(NbrVehicule)); } }      
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsGuideUnavailability { get => isGuideUnavailability; set { isGuideUnavailability = value;OnPropertyChanged(nameof(IsGuideUnavailability)); } }
        public bool IsEmplacementAvailable { get => isEmplacementAvailable; set { isEmplacementAvailable = value;OnPropertyChanged(nameof(IsEmplacementAvailable)); } }
        public bool IsInterestAvailable { get => isInterestAvailable; set { isInterestAvailable = value;OnPropertyChanged(nameof(IsInterestAvailable)); } }
    }
}
