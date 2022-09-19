using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.TranslationModel
{
    public class AddTranslateInterestVM : BaseModelVM
    {
        private string languageIso;
        private string content;
        private int interestId;
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso)); } }
        public string Content { get => content; set { content = value; OnPropertyChanged(Content); } }
        public int InterestId { get => interestId; set { interestId = value; OnPropertyChanged(nameof(InterestId)); } }
    }
}
