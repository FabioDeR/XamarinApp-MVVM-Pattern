using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.StepModel
{
    public class StepListOverviewVM : BaseModelVM
    {
        private string imageUrl;
        private int tourId;
        private string tourName;
        private string city;
        private string countryName;
        private int nbDays;
        private string tourContent;
        private List<DayVM> dayVMs;
        private string name;
        private string phone;
        private string mail;
        private string avatar;
        private float? pricePerAdult;
        private float? pricePerChild;
        private float? pricePerVehicule;
        private bool isShowEmplacement;
        private Guid guideId;
        public string ImageUrl { get => imageUrl; set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } }
        public int TourId { get => tourId; set { tourId = value; OnPropertyChanged(nameof(TourId)); } }
        public string TourName { get => tourName; set { tourName = value; OnPropertyChanged(nameof(TourName)); } }
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }
        public string CountryName { get => countryName; set { countryName = value; OnPropertyChanged(nameof(CountryName)); } }
        public int NbDays { get => nbDays; set { nbDays = value; OnPropertyChanged(nameof(NbDays)); } }
        public string TourContent { get => tourContent; set { tourContent = value; OnPropertyChanged(nameof(TourContent)); } }
        public List<DayVM> DayVMs { get => dayVMs; set { dayVMs = value; OnPropertyChanged(nameof(DayVMs)); } }
        public string Avatar { get => avatar; set { avatar = value; OnPropertyChanged(nameof(Avatar)); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(nameof(Phone)); } }
        public string Mail { get => mail; set { mail = value; OnPropertyChanged(nameof(Mail)); } }
        public float? PricePerVehicule { get => pricePerVehicule; set { pricePerVehicule = value; OnPropertyChanged(nameof(PricePerVehicule)); } }
        public float? PricePerChild { get => pricePerChild; set { pricePerChild = value; OnPropertyChanged(nameof(PricePerChild)); } }
        public float? PricePerAdult { get => pricePerAdult; set { pricePerAdult = value; OnPropertyChanged(nameof(PricePerAdult)); } }
        public bool IsShowEmplacement {get => isShowEmplacement; set { isShowEmplacement = value; OnPropertyChanged(nameof(IsShowEmplacement)); }}
        public Guid GuideId { get => guideId; set { guideId = value; OnPropertyChanged(nameof(GuideId)); } }
    }
    public class DayVM : BaseModelVM
    {
        private int day;
        private List<StepEditVM> stepEditVMs;

        public int Day { get => day; set { day = value; OnPropertyChanged(nameof(Day)); } }
        public List<StepEditVM> StepEditVMs { get => stepEditVMs; set { stepEditVMs = value; OnPropertyChanged(nameof(StepEditVMs)); } }


    }

}
