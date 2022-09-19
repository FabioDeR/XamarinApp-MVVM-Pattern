using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class ProfilEditVM : INotifyPropertyChanged
    {
        private Guid userId;

        [Required]
        [MaxLength(45)]
        private string nickname;
        [MaxLength(45)]
        private string firstname;
        [MaxLength(45)]
        private string lastname;
        private string description;
        private bool isAccessibility;
        private bool isGuide;
        private DateTime dateStartGuide;
        [MaxLength(150)]
        private string avatar;
        private DateTime born;
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        private string mail;
        [MaxLength(45)]
        private string address;
        [MaxLength(15)]
        [Phone]
        private string phone;
        [MaxLength(30)]
        private string city;
        [MaxLength(8)]
        private string zipCode;
        [MaxLength(30)]
        private string country;
        [Required]
        private string motherLanguage;
        private string anotherLanguage1;
        private string anotherLanguage2;
        private int rating;
        private string availabilities;
        private string preferedLanguage;
        private bool isPRM;
        public Guid UserId { get => userId; set { userId = value; OnPropertyChanged(); } }
        public string Nickname { get => nickname; set { nickname = value; OnPropertyChanged(); } }
        public string Firstname
        {
            get => firstname;
            set
            {
                firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public string ZipCode
        {
            get => zipCode;
            set
            {
                zipCode = value;
                OnPropertyChanged(nameof(ZipCode));
            }
        }
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public string Mail
        {
            get => mail;
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
        }
        public string Lastname
        {
            get => lastname;
            set
            {
                lastname = value;
                OnPropertyChanged("Lastname");
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public bool IsAccessibility
        {
            get => isAccessibility;
            set
            {
                isAccessibility = value;
                OnPropertyChanged("IsAccessibility");
            }
        }
        public bool IsGuide
        {
            get => isGuide;
            set
            {
                isGuide = value;
                OnPropertyChanged("IsGuide");
            }
        }
        public DateTime DateStartGuide
        {
            get => dateStartGuide;
            set
            {
                dateStartGuide = value;
                OnPropertyChanged("GuideStartDate");
            }
        }
        public string Avatar { get => avatar; set { avatar = value; OnPropertyChanged(nameof(Avatar)); } }
        public DateTime Born { get => born; set { born = value; OnPropertyChanged("Born"); } }
        public string MotherLanguage { get => motherLanguage; set { motherLanguage = value; OnPropertyChanged("MotherLanguage"); } }
        public string AnotherLanguage1 { get => anotherLanguage1; set { anotherLanguage1 = value; OnPropertyChanged("AnotherLanguage1"); } }
        public string AnotherLanguage2 { get => anotherLanguage2; set { anotherLanguage2 = value; OnPropertyChanged("AnotherLanguage2"); } }
        public int Rating { get => rating; set { rating = value;OnPropertyChanged(nameof(Rating)); } }
        public string Availabilities { get => availabilities; set { availabilities = value;OnPropertyChanged(nameof(Availabilities)); } }
        public string PreferedLanguage { get => preferedLanguage; set { preferedLanguage = value;OnPropertyChanged(nameof(PreferedLanguage)); } }
        public bool IsPRM { get => isPRM; set { isPRM = value;OnPropertyChanged(nameof(IsPRM)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
