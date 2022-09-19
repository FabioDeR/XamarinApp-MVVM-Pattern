using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        public INavigation Navigation { get;}
        public Page Page { get;}
        #region User Properties
        private string motherLanguage;
        public string MotherLanguage { get => motherLanguage; set { motherLanguage = value; OnPropertyChanged(nameof(MotherLanguage)); } }
        private Guid userId;
        public Guid UserId { get => userId; set { userId = value; OnPropertyChanged(nameof(UserId)); } }
        #endregion
        public ICommand Save { get; protected set; }
        public ICommand Post { get; protected set; }
        public ICommand GoTo { get; protected set; }
        public ICommand GoToDetail { get; protected set; }
        public ICommand GoToTwo { get; protected set; }
        public ICommand Delete { get; protected set; }
        public ICommand Update { get; protected set; }
        public ICommand UpdateTwo { get; protected set; }
        public ICommand PostPhoto { get; protected set; }
        public BaseVM(INavigation navigation, Page page)
        {
            Navigation = navigation;
            Page = page;
            #region User instance
            MotherLanguage = App.Database.GetUserMotherLanguage();
            UserId = App.Database.GetUserId();
            #endregion
        }
        #region Porperty Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
