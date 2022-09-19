using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TabiTravel.XamarinModel.Models
{
    public class GetCategoryVM : INotifyPropertyChanged
    {
        private int categoryId;
        private string name;
        private bool isSelected;
        public int CategoryId { get => categoryId; set { categoryId = value; OnPropertyChanged(nameof(CategoryId));}}
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name));}}
        public bool IsSelected { get => isSelected; set { isSelected = value;OnPropertyChanged(nameof(IsSelected));}}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
