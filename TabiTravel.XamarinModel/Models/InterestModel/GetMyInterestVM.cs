using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.InterestModel
{
    public class GetMyInterestVM :BaseModelVM
    {
        private int interestId;
        private string name;
        private string city;
        private string imageUrl;
        private string country;
        private string content;
        private List<string> languages;
        private List<string> categories;
        private Guid userId;

        public int InterestId { get => interestId; set { interestId = value; OnPropertyChanged(nameof(InterestId)); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }
        public string ImageUrl { get => imageUrl; set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } }
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public List<string> Languages { get => languages; set { languages = value; OnPropertyChanged(nameof(Languages)); } }
        public List<string> Categories { get => categories; set { categories = value; OnPropertyChanged(nameof(Categories)); } }
        public Guid UserId { get => userId; set { userId = value;OnPropertyChanged(nameof(UserId)); } }
    }
}
