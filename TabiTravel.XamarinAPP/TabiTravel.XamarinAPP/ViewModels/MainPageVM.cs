using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public MainPageVM(INavigation _navigation)
        {
            Navigation = _navigation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
