using System;
using System.Collections.Generic;
using System.Text;
using TabiTravel.XamarinModel.Enum;

namespace TabiTravel.XamarinModel.Models
{
    public class SearchVM : BaseModelVM
    {
        //Accessibility
        private bool isPRMFriendly;
        private bool isStrollerFriendly;
        private bool isChildFriendly;
        private bool isAnimalFriendly;

        //Price
        private bool isFree;
        private float? pricePerAdult;
        private float? pricePerChild;
        private float? pricePerVehicule;

        //Days
        private bool isMonday;
        private bool isTuesday;
        private bool isWednesday;
        private bool isThursday;
        private bool isFriday;
        private bool isSaturday;
        private bool isSunday;
        private DateTime date;

        //Other
        private string name;
        private string city;
        private int estimatedTime;
        private string languageIso;

        //Category
        private int categoryId;

        //Interest And/Or Tour   1 = Interst    2 = Tour    3 = All
        private EnumSearch enumSearch;

        public bool IsPRMFriendly { get => isPRMFriendly; set { isPRMFriendly = value; OnPropertyChanged(nameof(IsPRMFriendly));} }
        public bool IsStrollerFriendly { get => isStrollerFriendly; set { isStrollerFriendly = value; OnPropertyChanged(nameof(IsStrollerFriendly));} }
        public bool IsChildFriendly { get => isChildFriendly; set { isChildFriendly = value; OnPropertyChanged(nameof(IsChildFriendly));} }
        public bool IsAnimalFriendly { get => isAnimalFriendly; set { isAnimalFriendly = value; OnPropertyChanged(nameof(IsAnimalFriendly));} }
        public bool IsFree { get => isFree; set { isFree = value; OnPropertyChanged(nameof(IsFree));} }
        public float? PricePerAdult { get => pricePerAdult; set { pricePerAdult = value; OnPropertyChanged(nameof(PricePerAdult));} }
        public float? PricePerChild { get => pricePerChild; set { pricePerChild = value; OnPropertyChanged(nameof(PricePerChild));} }
        public float? PricePerVehicule { get => pricePerVehicule; set { pricePerVehicule = value; OnPropertyChanged(nameof(PricePerVehicule));} }
        public bool IsMonday { get => isMonday; set { isMonday = value; OnPropertyChanged(nameof(IsMonday));} }
        public bool IsTuesday { get => isTuesday; set { isTuesday = value; OnPropertyChanged(nameof(IsThursday));} }
        public bool IsWednesday { get => isWednesday; set { isWednesday = value; OnPropertyChanged(nameof(IsWednesday));} }
        public bool IsThursday { get => isThursday; set { isThursday = value; OnPropertyChanged(nameof(IsThursday));} }
        public bool IsFriday { get => isFriday; set { isFriday = value; OnPropertyChanged(nameof(IsFriday));} }
        public bool IsSaturday { get => isSaturday; set { isSaturday = value; OnPropertyChanged(nameof(IsSaturday));} }
        public bool IsSunday { get => isSunday; set { isSunday = value; OnPropertyChanged(nameof(IsSunday));} }
        public DateTime Date { get => date; set { date = value; OnPropertyChanged(nameof(Date));} }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name));} }
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City));} }
        public int EstimatedTime { get => estimatedTime; set { estimatedTime = value; OnPropertyChanged(nameof(EstimatedTime));} }
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso));} }
        public int CategoryId { get => categoryId; set { categoryId = value; OnPropertyChanged(nameof(CategoryId));} }
        public EnumSearch EnumSearch { get => enumSearch; set { enumSearch = value; OnPropertyChanged(nameof(EnumSearch));} }
    }
}
