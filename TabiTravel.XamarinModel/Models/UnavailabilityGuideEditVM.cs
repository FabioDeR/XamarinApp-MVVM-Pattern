using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class UnavailabilityGuideEditVM : INotifyPropertyChanged
    {
        private int unavailabilityGuideId;
        private DateTime dateStart;
        private DateTime dateEnd;
        private Guid userId;


        public int UnavailabilityGuideId { get => unavailabilityGuideId; set { unavailabilityGuideId = value; OnPropertyChanged(nameof(UnavailabilityGuideId)); } }
        public DateTime DateStart { get => dateStart; set { dateStart = value.Date.Date; OnPropertyChanged(nameof(DateStart)); } }
        public DateTime DateEnd { get => dateEnd; set { dateEnd = value.Date.Date; OnPropertyChanged(nameof(DateEnd)); } }
        public Guid UserId { get => userId; set { userId = value; OnPropertyChanged(nameof(UserId)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
