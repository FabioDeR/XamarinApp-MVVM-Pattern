using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class User : INotifyPropertyChanged
    {
        private Guid id;

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
        private DateTime guideStartDate;

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

        public Guid Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string Nickname { get => nickname; set { nickname = value; OnPropertyChanged(Nickname); } }
        public string Firstname
        {
            get => firstname; 
            set
            {
                firstname = value;
                OnPropertyChanged("Firstname");
            }
        }
        public string Country
        {
            get => country; 
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }
        public string ZipCode
        {
            get => zipCode; 
            set
            {
                zipCode = value;
                OnPropertyChanged("ZipCode");
            }
        }
        public string City
        {
            get => city; 
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        public string Phone
        {
            get => phone; 
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
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
        public DateTime GuideStartDate
        {
            get => guideStartDate; 
            set
            {
                guideStartDate = value;
                OnPropertyChanged("GuideStartDate");
            }
        }

        public string Avatar { get => avatar; set { avatar = value; OnPropertyChanged("Avatar"); } }
        public DateTime Born { get => born; set { born = value; OnPropertyChanged("Born"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
