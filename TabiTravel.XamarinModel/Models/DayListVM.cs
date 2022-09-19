using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class DayListVM : BaseModelVM
    {
        private int _dayId;
        private string _dayName;
        private AvailabilityGuideDayVM _availabilityGuideDayVM;

        public int DayId { get => _dayId; set { _dayId = value; OnPropertyChanged(nameof(DayId)); } }
        public string DayName { get => _dayName; set { _dayName = value; OnPropertyChanged(nameof(DayName)); } }
        public AvailabilityGuideDayVM AvailabilityGuideDayVM { get => _availabilityGuideDayVM; set { _availabilityGuideDayVM = value; OnPropertyChanged(nameof(AvailabilityGuideDayVM)); } }

    }

    public class AvailabilityGuideDayVM : BaseModelVM
    {
        private int _availabilityGuideId;
        private string _morning;
        private bool _isMorning;
        private string _afternoon;
        private bool _isAfternoon;
        private string _evening;
        private bool _isEvening;

        public int AvailabilityGuideId { get => _availabilityGuideId; set { _availabilityGuideId = value; OnPropertyChanged(nameof(AvailabilityGuideId)); } }
        public string Morning { get => _morning; set { _morning = value; OnPropertyChanged(nameof(Morning)); } }
        public bool IsMorning { get => _isMorning; set { _isMorning = value; OnPropertyChanged(nameof(IsMorning)); } }
        public string Afternoon { get => _afternoon; set { _afternoon = value; OnPropertyChanged(nameof(Afternoon)); } }
        public bool IsAfternoon { get => _isAfternoon; set { _isAfternoon = value; OnPropertyChanged(nameof(IsAfternoon)); } }
        public string Evening { get => _evening; set { _evening = value; OnPropertyChanged(nameof(Evening)); } }
        public bool IsEvening { get => _isEvening; set { _isEvening = value; OnPropertyChanged(nameof(IsEvening)); } }

    }
}
