using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.TourModel
{
    public class TourEditVM : BaseModelVM
    {
        private Tour tour;
        private string languageIso;
        private string content;
        private string country;
        private Guid guideId;
        private string name;
        private string nickName;
        private string avatar;
        private List<GetCategoryVM> categories;
        public Tour Tour { get => tour; set { tour = value; OnPropertyChanged(nameof(Tour)); } }
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso)); } }
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        public List<GetCategoryVM> Categories { get => categories; set { categories = value; OnPropertyChanged(nameof(Categories)); } }       
        public string NickName { get => nickName; set { nickName = value; OnPropertyChanged(nameof(NickName)); } }
        public string Avatar { get => avatar; set { avatar = value; OnPropertyChanged(nameof(Avatar)); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public Guid GuideId { get => guideId; set { guideId = value; OnPropertyChanged(nameof(GuideId)); } }
    }
}
