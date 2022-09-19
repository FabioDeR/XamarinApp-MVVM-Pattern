using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models.StepModel
{
    public class GetMyStepVM : INotifyPropertyChanged
    {
        private int stepId;
        private string imageUrl;
        private string stepName;
        private string city;
        private string country;
        private string content;
        private int interestId;

        public int StepId { get => stepId; set { stepId = value; OnPropertyChanged(nameof(StepId)); } }
        public string ImageUrl { get => imageUrl; set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); } }
        public string StepName { get => stepName; set { stepName = value; OnPropertyChanged(nameof(StepName)); } }
        public string City { get => city; set { city = value; OnPropertyChanged(nameof(City)); } }
        public string Country { get => country; set { country = value; OnPropertyChanged(nameof(Country)); } }
        public string Content { get => content; set { content = value; OnPropertyChanged(nameof(Content)); } }
        public int InterestId { get => interestId; set { interestId = value; OnPropertyChanged(nameof(InterestId)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
