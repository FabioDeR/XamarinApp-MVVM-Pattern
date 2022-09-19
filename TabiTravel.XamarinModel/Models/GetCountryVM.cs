using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class GetCountryVM : INotifyPropertyChanged
    {
        private int countryId;
        private string name;

        public int CountryId { get => countryId; set { countryId = value; OnPropertyChanged(nameof(CountryId)); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
