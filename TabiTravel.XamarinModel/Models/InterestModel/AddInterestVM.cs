using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.InterestModel
{
    public class AddInterestVM : BaseModelVM
    {

        private Interest interest;
        private string languageIso;
        private string content;
        private string country;
        private string monday;
        private string tuesday;
        private string wednesday;
        private string thursday;
        private string friday;
        private string saturday;
        private string sunday;
        private List<GetCategoryVM> categories;
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso)); } }
        public Interest Interest { get => interest; set { interest = value; OnPropertyChanged(nameof(Interest)); } }
        public List<GetCategoryVM> Categories { get => categories; set { categories = value; OnPropertyChanged(nameof(Categories)); } }
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        public string Monday { get => monday; set { monday = value; OnPropertyChanged(nameof(Monday)); } }
        public string Tuesday { get => tuesday; set { tuesday = value; OnPropertyChanged(nameof(Tuesday)); } }
        public string Wednesday { get => wednesday; set { wednesday = value; OnPropertyChanged(nameof(Wednesday)); } }
        public string Thursday { get => thursday; set { thursday = value; OnPropertyChanged(nameof(Thursday)); } }
        public string Friday { get => friday; set { friday = value; OnPropertyChanged(nameof(Friday)); } }
        public string Saturday { get => saturday; set { saturday = value; OnPropertyChanged(nameof(Saturday)); } }
        public string Sunday { get => sunday; set { sunday = value; OnPropertyChanged(nameof(Sunday)); } }

    }
}
