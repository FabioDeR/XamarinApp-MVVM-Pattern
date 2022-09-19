using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.TourModel
{
    public class Tour : BaseModelVM
    {
        private int id;
        private string city;
        private string imageUrl;
        private Guid userId;
        private int countryId;
        private string name;
        private bool validateAdmin;
        private DateTime? dateToValidation;
        private float? pricePerAdult;
        private float? pricePerChild;
        private float? pricePerVehicule;
        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        [Required]
        [MaxLength(45)]
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        [MaxLength(45)]
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }
        public string ImageUrl { get => imageUrl; set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } }
        public Guid UserId { get => userId; set { userId = value; OnPropertyChanged(nameof(UserId)); } }
        [Range(1, int.MaxValue, ErrorMessage = "A Country must be selected")]
        public int CountryId { get => countryId; set { countryId = value; OnPropertyChanged(nameof(CountryId)); } }
        public float? PricePerAdult { get => pricePerAdult; set { pricePerAdult = value; OnPropertyChanged(nameof(PricePerAdult)); } }
        public float? PricePerChild { get => pricePerChild; set { pricePerChild = value; OnPropertyChanged(nameof(PricePerChild)); } }
        public float? PricePerVehicule { get => pricePerVehicule; set { pricePerVehicule = value; OnPropertyChanged(nameof(PricePerVehicule)); } }
        public bool ValidateAdmin { get => validateAdmin; set {  validateAdmin = value; OnPropertyChanged(nameof(ValidateAdmin)); } }
        public DateTime? DateToValidation { get => dateToValidation; set { dateToValidation = value; OnPropertyChanged(nameof(DateToValidation)); } }
    }
}
