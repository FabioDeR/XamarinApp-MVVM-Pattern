using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.TourModel
{
    public class GetMyTourListVM : BaseModelVM
    {
        private int tourId;
        private string name;
        private string city;
        private string imageUrl;
        private string country;
        private string content;
        private List<string> languages;
        private List<string> categories;
        private float? pricePerAdult;
        private float? pricePerChild;
        private float? pricePerVehicule;
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public List<string> Categories { get => categories; set { categories = value; OnPropertyChanged(nameof(Categories)); } }
        public List<string> Languages { get => languages; set { languages = value; OnPropertyChanged(nameof(Languages)); } }
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        public string ImageUrl { get => imageUrl; set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } }
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public int TourId { get => tourId; set { tourId = value; OnPropertyChanged(nameof(TourId)); } }
        public float? PricePerVehicule { get => pricePerVehicule; set { pricePerVehicule = value;OnPropertyChanged(nameof(PricePerVehicule)); } }
        public float? PricePerChild { get => pricePerChild; set { pricePerChild = value; OnPropertyChanged(nameof(PricePerChild)); } }
        public float? PricePerAdult { get => pricePerAdult; set { pricePerAdult = value; OnPropertyChanged(nameof(PricePerAdult)); } }
    }
}
