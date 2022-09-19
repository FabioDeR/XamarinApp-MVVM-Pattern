using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.StepModel
{
    public class StepEditVM : INotifyPropertyChanged
    {
        private int day;
        private string name;
        private string content;
        private float? previousStep;
        private string languageIso;
        private int interestId;
        private int tourId;
        private int position;
        private int stepId;
        private string unit;
        private int unitId;
        private string interestName;
        private string interestImage;

        public string InterestName { get => interestName; set { interestName = value; OnPropertyChanged(nameof(InterestName)); } }
        public string InterestImage { get => interestImage; set { interestImage = value; OnPropertyChanged(nameof(InterestImage)); } }

        public int Day
        {
            get => day; set { day = value; OnPropertyChanged(nameof(Day)); }
        }
        [Required]
        [MaxLength(45)]
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public float? PreviousStep { get => previousStep; set { previousStep = value; OnPropertyChanged(nameof(PreviousStep)); } }
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso)); } }
        [Range(0, int.MaxValue, ErrorMessage = "A Point of interest must be selected")]
        public int InterestId { get => interestId; set { interestId = value; OnPropertyChanged(nameof(InterestId)); } }
        public int TourId { get => tourId; set { tourId = value; OnPropertyChanged(nameof(TourId)); } }
        public int Position { get => position; set { position = value; OnPropertyChanged(nameof(Position)); } }
        public int StepId { get => stepId; set { stepId = value; OnPropertyChanged(nameof(StepId)); } }
        public string Unit { get => unit; set { unit = value; OnPropertyChanged(nameof(Unit)); } }
        public int UnitId { get => unitId; set { unitId = value; OnPropertyChanged(nameof(UnitId)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
