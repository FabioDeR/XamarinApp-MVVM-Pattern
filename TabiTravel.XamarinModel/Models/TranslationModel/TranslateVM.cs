using System;
using System.Collections.Generic;
using System.Text;

namespace TabiTravel.XamarinModel.Models.TranslationModel
{
    public class TranslateVM : BaseModelVM
    {
        private int translateId;
        private string languageIso;
        private string content;
        private bool motherLanguage;
        private int interestId;
        private int stepId;
        private int tourId;

        public int TranslateId { get => translateId; set { translateId = value; OnPropertyChanged(nameof(TranslateId)); } }
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso)); } }
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public bool MotherLanguage { get => motherLanguage; set { motherLanguage = value; OnPropertyChanged(nameof(MotherLanguage)); } }
        public int InterestId { get => interestId; set { interestId = value; OnPropertyChanged(nameof(InterestId)); } }
        public int StepId { get => stepId; set { stepId = value; OnPropertyChanged(nameof(StepId)); } }
        public int TourId { get => tourId; set { tourId = value; OnPropertyChanged(nameof(TourId)); } }
    }
}
