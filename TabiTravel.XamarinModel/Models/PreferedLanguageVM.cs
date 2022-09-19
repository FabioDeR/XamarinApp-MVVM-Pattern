using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class PreferedLanguageVM : INotifyPropertyChanged
    {
        private Guid userId;
        private string languageIso;
        private bool isSelected;

        public Guid UserId { get => userId; set { userId = value; OnPropertyChanged(nameof(UserId)); } }
        public string LanguageIso { get => languageIso; set { languageIso = value; OnPropertyChanged(nameof(LanguageIso)); } }
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
