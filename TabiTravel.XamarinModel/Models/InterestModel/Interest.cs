using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.InterestModel
{
    public class Interest : BaseModelVM
    {
        private int id;
        private string name;
        private string email;
        private string address;
        private string city;
        private bool isPRMFriendly;
        private bool isStrollerFriendly;
        private bool isChildFriendly;
        private bool isAnimalFriendly;
        private bool isFree;
        private bool isFreeParking;
        private float? pricePerAdult;
        private float? pricePerChild;
        private float? pricePerVehicule;
        private int? emplacementAvailable;
        private string url;
        private string schedule;
        private bool isMonday;
        private bool isTuesday;
        private bool isWednesday;
        private bool isThursday;
        private bool isFriday;
        private bool isSaturday;
        private bool isSunday;
        private double longitude;
        private double latitude;
        private string imageUrl;
        private string zipCode;
        private string phone;
        private bool validateAdmin;
        private DateTime? dateToValidation;
        private Guid userId;
        private int countryId;
        private int? estimatedTime;



        public bool IsMonday { get => isMonday; set { isMonday = value; OnPropertyChanged(nameof(IsMonday)); } }
        public bool IsTuesday { get => isTuesday; set { isTuesday = value; OnPropertyChanged(nameof(IsTuesday)); } }
        public bool IsWednesday { get => isWednesday; set { isWednesday = value; OnPropertyChanged(nameof(IsWednesday)); } }
        public bool IsThursday { get => isThursday; set { isThursday = value; OnPropertyChanged(nameof(IsThursday)); } }
        public bool IsFriday { get => isFriday; set { isFriday = value; OnPropertyChanged(nameof(IsFriday)); } }
        public bool IsSaturday { get => isSaturday; set { isSaturday = value; OnPropertyChanged(nameof(IsSaturday)); } }
        public bool IsSunday { get => isSunday; set { isSunday = value; OnPropertyChanged(nameof(IsSunday)); } }
        public int? EstimatedTime { get => estimatedTime; set { estimatedTime = value; OnPropertyChanged(nameof(EstimatedTime)); } }       
        public int CountryId { get => countryId; set { countryId = value; OnPropertyChanged(nameof(CountryId)); } }
        public Guid UserId { get => userId; set { userId = value; OnPropertyChanged(nameof(UserId)); } }
        public DateTime? DateToValidation { get => dateToValidation; set { dateToValidation = value; OnPropertyChanged(nameof(DateToValidation)); } }
        public bool ValidateAdmin { get => validateAdmin; set { validateAdmin = value; OnPropertyChanged(nameof(ValidateAdmin)); } }
        [Phone]
        [MaxLength(15)]
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(nameof(Phone)); } }
        [MaxLength(8)]
        public string ZipCode { get => zipCode; set { zipCode = value; OnPropertyChanged(nameof(ZipCode)); } }
        [MaxLength(150)]
        public string ImageUrl { get => imageUrl; set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } }
        [Required]
        [Range(-180, 180)]
        public double Latitude { get => latitude; set { latitude = value; OnPropertyChanged(nameof(Latitude)); } }
        [Required]
        [Range(-180, 180)]
        public double Longitude { get => longitude; set { longitude = value; OnPropertyChanged(nameof(Longitude)); } }
        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        [Required]
        [MaxLength(45)]
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        [MaxLength(100)]
        public string Email { get => email; set { email = value; OnPropertyChanged(nameof(Email)); } }
        [MaxLength(45)]
        public string Address { get => address; set { address = value; OnPropertyChanged(nameof(Address)); } }
        [MaxLength(45)]
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }
        public bool IsPRMFriendly { get => isPRMFriendly; set { isPRMFriendly = value; OnPropertyChanged(nameof(IsPRMFriendly)); } }
        public bool IsStrollerFriendly { get => isStrollerFriendly; set { isStrollerFriendly = value; OnPropertyChanged(nameof(IsStrollerFriendly)); } }
        public bool IsChildFriendly { get => isChildFriendly; set { isChildFriendly = value; OnPropertyChanged(nameof(IsChildFriendly)); } }
        public bool IsAnimalFriendly { get => isAnimalFriendly; set { isAnimalFriendly = value; OnPropertyChanged(nameof(IsAnimalFriendly)); } }
        public bool IsFree { get => isFree; set { isFree = value; OnPropertyChanged(nameof(IsFree)); } }
        public bool IsFreeParking { get => isFreeParking; set { isFreeParking = value; OnPropertyChanged(nameof(IsFreeParking)); } }
        [Range(0, float.MaxValue)]
        public float? PricePerAdult { get => pricePerAdult; set { pricePerAdult = value; OnPropertyChanged(nameof(PricePerAdult)); } }
        [Range(0, float.MaxValue)]
        public float? PricePerChild { get => pricePerChild; set { pricePerChild = value; OnPropertyChanged(nameof(PricePerChild)); } }
        [Range(0, float.MaxValue)]
        public float? PricePerVehicule { get => pricePerVehicule; set { pricePerVehicule = value; OnPropertyChanged(nameof(PricePerVehicule)); } }   
        [Range(1,int.MaxValue,ErrorMessage ="Mininum 1 emplacement is required")]
        public int? EmplacementAvailable { get => emplacementAvailable; set { emplacementAvailable = value; OnPropertyChanged(nameof(EmplacementAvailable)); } }
        public string Url { get => url; set { url = value; OnPropertyChanged(nameof(Url)); } }
        [MaxLength(100)]
        public string Schedule { get => schedule; set { schedule = value; OnPropertyChanged(nameof(Schedule)); } }
    }
}
